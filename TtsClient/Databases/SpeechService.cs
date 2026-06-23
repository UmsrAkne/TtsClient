using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class SpeechService
    {
        private readonly MyDbContext context;

        public SpeechService(MyDbContext context)
        {
            this.context = context;
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
    }
}