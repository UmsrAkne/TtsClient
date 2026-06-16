using System.Collections.Generic;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public interface ISpeechMetadataRepository
    {
        Task<IEnumerable<SpeechMetadata>> GetAllAsync();

        Task AddAsync(SpeechMetadata entry);

        Task SaveAsync();
    }
}