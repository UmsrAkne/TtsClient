using System.Threading.Tasks;

namespace TtsClient.TtsEngine
{
    public interface ITtsEngine
    {
        public string EngineName { get; }

        Task<byte[]> SynthesizeAsync(TtsRequest request);

        string GenerateSsmlText(string entryTitle, string entryContents);
    }
}