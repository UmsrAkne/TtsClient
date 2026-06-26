using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;
using TtsClient.Services;
using TtsClient.TtsEngine;
using TtsClient.Utils;

namespace TtsClient.Databases
{
    public class SpeechService
    {
        private readonly MyDbContext context;
        private readonly YamlSpeechParser yamlSpeechParser;

        public SpeechService(MyDbContext context, YamlSpeechParser yamlSpeechParser)
        {
            this.context = context;
            this.yamlSpeechParser = yamlSpeechParser;
        }

        public async Task<IEnumerable<SpeechEntry>> GetHistoryAsync(int limit = 500)
        {
            // OrderBy を C# 側から実行できないため、生のSQLで代用する。
            var sql = "SELECT * FROM SpeechEntries ORDER BY CreatedAt DESC LIMIT @p0";
            return await context.SpeechEntries
                .FromSqlRaw(sql, limit)
                .Include(s => s.AudioFiles)
                .ToListAsync();
        }

        public async Task<IEnumerable<SpeechEntry>> GetSpeechEntries(DateTime fromDate)
        {
            return await context.SpeechEntries
                .Where(s => s.CreatedAt > fromDate)
                .OrderBy(s => s.CreatedAt)
                .Include(s => s.AudioFiles)
                .ToListAsync();
        }

        public async Task RegisterEntryAsync(SpeechEntry entry)
        {
            context.SpeechEntries.Add(entry);
            await context.SaveChangesAsync();
        }

        public async Task RegisterMetadataRangeAsync(IEnumerable<SpeechMetadata> speechMetadataList)
        {
            // foreachで毎回AddAsyncを呼ぶ必要はなく、AddRangeで一括追加
            context.SpeechMetadata.AddRange(speechMetadataList);
            await context.SaveChangesAsync();
        }

        public async Task RegisterAudioFileEntryAsync(AudioFileEntry entry)
        {
            context.AudioFileEntries.Add(entry);
            await context.SaveChangesAsync();
        }

        public async Task<SpeechEntry> ProcessSingleTextAsync(string rawText, DateTime executionTime, TtsService ttsService)
        {
            // 1. パース
            var results = yamlSpeechParser.Parse(rawText);
            var entry = new SpeechEntry()
            {
                Title = results.Title,
                Contents = results.Contents,
            };

            var titleCall = results.Metadata.FirstOrDefault(kv => kv.Key == "TitleCall").Value ?? string.Empty;

            // 2. TTSリクエスト作成 & 通信
            var req = new TtsRequest
            {
                Text = ttsService.GetSsmlTextWithTitleCall(entry.Title, entry.Contents, titleCall),
                Voice = "ja-JP-Wavenet-D",
            };
            var byteArray = await ttsService.SynthesizeAsync(req);

            // 3. ファイル保存
            var dateDirName = executionTime.ToString("yyyy-MM-dd");
            var fileName = $"{executionTime:yyyyMMdd_HHmmss_fff}.mp3";
            var path = Path.Combine(PathHelper.AudioDirectoryName, dateDirName, fileName);
            var absoluteDirPath = Path.Combine(AppContext.BaseDirectory, PathHelper.AudioDirectoryName, dateDirName);

            Directory.CreateDirectory(absoluteDirPath);
            await File.WriteAllBytesAsync(Path.Combine(absoluteDirPath, fileName), byteArray);

            // 4. データ登録（一連のトランザクションにできるとより安全）
            await RegisterEntryAsync(entry);

            var audioFileEntry = new AudioFileEntry
            {
                SpeechEntryId = entry.Id,
                AudioRelativePath = path,
                ProcessedSsml = req.Text,
                Status = SpeechEntryStatus.Pending,
                GeneratedAt = executionTime,
            };
            await RegisterAudioFileEntryAsync(audioFileEntry);

            var metadataList = results.Metadata.Select(kv => new SpeechMetadata()
            {
                Key = kv.Key,
                Value = kv.Value,
                SpeechEntryId = entry.Id,
            });
            await RegisterMetadataRangeAsync(metadataList);

            return entry;
        }
    }
}