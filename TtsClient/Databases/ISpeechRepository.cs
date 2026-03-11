using System.Collections.Generic;
using System.Threading.Tasks;
using TtsClient.Models;

namespace TtsClient.Databases
{
    public interface ISpeechRepository
    {
        Task<IEnumerable<SpeechEntry>> GetAllAsync();

        Task AddAsync(SpeechEntry entry);

        Task SaveAsync();
    }
}