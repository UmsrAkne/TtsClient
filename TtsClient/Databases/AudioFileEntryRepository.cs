using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class AudioFileEntryRepository : IAudioFileEntryRepository
    {
        private readonly MyDbContext context;

        public AudioFileEntryRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AudioFileEntry>> GetAllAsync()
        {
            var list = await context.AudioFileEntries.ToListAsync();
            return list;
        }

        public async Task AddAsync(AudioFileEntry entry)
        {
            await context.AudioFileEntries.AddAsync(entry);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}