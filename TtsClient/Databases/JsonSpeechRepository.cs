using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TtsClient.Models;
using TtsClient.Utils;

namespace TtsClient.Databases
{
    public class JsonSpeechRepository : ISpeechRepository
    {
        private readonly string jsonFilePath = PathHelper.GetSpeechEntriesJsonFilePath();
        private readonly List<SpeechEntry> entries = new ();

        public JsonSpeechRepository()
        {
            // 起動時に既存のデータを読み込んでおく
            if (File.Exists(jsonFilePath))
            {
                try
                {
                    var json = File.ReadAllText(jsonFilePath);
                    var deserialized = JsonSerializer.Deserialize<List<SpeechEntry>>(json);
                    if (deserialized != null)
                    {
                        entries.AddRange(deserialized);
                    }
                }
                catch (JsonException)
                {
                    // ファイルが壊れている場合などのハンドリング（ログ出力など）
                    Logger.Log("Failed to load speech entries from JSON file.");
                }
            }
        }

        public Task<IEnumerable<SpeechEntry>> GetAllAsync()
        {
            // 読み取り専用としてリストを返す
            return Task.FromResult(entries.AsEnumerable());
        }

        public Task AddAsync(SpeechEntry entry)
        {
            // リストに追加するだけ（保存は SaveAsync を呼ぶまで行わない設計）
            entries.Add(entry);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,

                // 日本語が文字化けしないようにエンコーディングを指定
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            };

            var json = JsonSerializer.Serialize(entries, options);
            await File.WriteAllTextAsync(jsonFilePath, json);
        }
    }
}