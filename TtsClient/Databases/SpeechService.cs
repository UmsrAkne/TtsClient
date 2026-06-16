using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class SpeechService
    {
        private readonly ISpeechRepository repository;
        private readonly ISpeechMetadataRepository metadataRepository;

        // コンストラクタでリポジトリを注入（DI）
        public SpeechService(ISpeechRepository repository, ISpeechMetadataRepository metadataRepository)
        {
            this.repository = repository;
            this.metadataRepository = metadataRepository;
        }

        public async Task<IEnumerable<SpeechEntry>> GetHistoryAsync(int limit = 500)
        {
            var entries = await repository.GetAllAsync();
            return entries.OrderByDescending(s => s.CreatedAt)
                .Take(limit);
        }

        public async Task<IEnumerable<SpeechEntry>> GetSpeechEntries(DateTimeOffset fromDate)
        {
            var entries = await repository.GetAllAsync();
            entries = entries.Where(s => s.CreatedAt > fromDate);
            return entries.OrderBy(s => s.CreatedAt);
        }

        public async Task RegisterEntryAsync(SpeechEntry entry)
        {
            await repository.AddAsync(entry);
            await repository.SaveAsync();
        }

        public async Task RegisterMetadataRangeAsync(IEnumerable<SpeechMetadata> speechMetadataList)
        {
            foreach (var speechMetadata in speechMetadataList)
            {
                await metadataRepository.AddAsync(speechMetadata);
            }

            await metadataRepository.SaveAsync();
        }
    }
}