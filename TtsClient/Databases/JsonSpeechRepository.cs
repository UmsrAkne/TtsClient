using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class JsonSpeechRepository : ISpeechRepository
    {
        private readonly string filePath;
        private List<SpeechEntry> entries = new();

        public async Task<IEnumerable<SpeechEntry>> GetAllAsync()
        {
            // JSONのパース自体はCPU処理ですが、
            // 将来のDBアクセスを模してTaskで返します
            return await Task.FromResult(entries);
        }

        public Task AddAsync(SpeechEntry entry)
        {
            // Todo: 仮実装
            entries.Add(entry);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, };
            var json = JsonSerializer.Serialize(entries, options);

            // ファイル書き込みを非同期で行う
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}