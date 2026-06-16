using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class SpeechMetadataRepository : ISpeechMetadataRepository
    {
        private readonly MyDbContext context;

        public SpeechMetadataRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SpeechMetadata>> GetAllAsync()
        {
            var list = await context.SpeechMetadata.ToListAsync();
            return list;
        }

        public async Task AddAsync(SpeechMetadata entry)
        {
            await context.SpeechMetadata.AddAsync(entry);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}