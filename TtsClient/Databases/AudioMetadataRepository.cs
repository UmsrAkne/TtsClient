using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public class AudioMetadataRepository : IAudioMetadataRepository
    {
        private readonly MyDbContext context;

        public AudioMetadataRepository(MyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AudioMetadata>> GetAllAsync()
        {
            var list = await context.AudioMetadata.ToListAsync();
            return list;
        }

        public async Task AddAsync(AudioMetadata entry)
        {
            await context.AudioMetadata.AddAsync(entry);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}