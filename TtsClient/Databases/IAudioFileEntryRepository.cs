using System.Collections.Generic;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public interface IAudioFileEntryRepository
    {
        Task<IEnumerable<AudioFileEntry>> GetAllAsync();

        Task AddAsync(AudioFileEntry entry);

        Task SaveAsync();
    }
}