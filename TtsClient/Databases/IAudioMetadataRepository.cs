using System.Collections.Generic;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public interface IAudioMetadataRepository
    {
        Task<IEnumerable<AudioMetadata>> GetAllAsync();

        Task AddAsync(AudioMetadata entry);

        Task SaveAsync();
    }
}