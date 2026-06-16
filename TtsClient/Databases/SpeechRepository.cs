using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class SpeechRepository : ISpeechRepository
    {
        private readonly MyDbContext context;

        public SpeechRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SpeechEntry>> GetAllAsync()
        {
            var list = await context.SpeechEntries.ToListAsync();
            return list;
        }

        public async Task AddAsync(SpeechEntry entry)
        {
            await context.SpeechEntries.AddAsync(entry);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}