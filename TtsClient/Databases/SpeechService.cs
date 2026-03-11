using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class SpeechService
    {
        private readonly ISpeechRepository repository;

        // コンストラクタでリポジトリを注入（DI）
        public SpeechService(ISpeechRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<SpeechEntry>> GetHistoryAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task RegisterEntryAsync(string rawText, string audioPath)
        {
            var entry = new SpeechEntry
            {
                Id = Guid.NewGuid(),
                RawText = rawText,
                AudioPath = audioPath,
                CreatedAt = DateTimeOffset.Now,
            };

            await repository.AddAsync(entry);
            await repository.SaveAsync();
        }
    }
}