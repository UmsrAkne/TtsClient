using System.Threading.Tasks;

namespace TtsClient.TtsEngine
{
    public interface ITtsEngine
    {
        public string EngineName { get; }

        Task<byte[]> SynthesizeAsync(TtsRequest request);
    }
}