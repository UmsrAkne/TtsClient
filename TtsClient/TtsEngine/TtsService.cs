using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TtsClient.TtsEngine
{
    public class TtsService
    {
        public TtsService(IEnumerable<ITtsEngine> ttsEngines)
        {
            AvailableEngines = ttsEngines;
            CurrentEngine = AvailableEngines.FirstOrDefault();
        }

        // 現在選択されているエンジン（これが Strategy の切り替え地点）
        public ITtsEngine CurrentEngine { get; set; }

        public IEnumerable<ITtsEngine> AvailableEngines { get; set; }

        public Task<byte[]> SynthesizeAsync(TtsRequest request)
        {
            // 現在のエンジンに処理を委譲（デリゲート）する
            return CurrentEngine.SynthesizeAsync(request);
        }
    }
}