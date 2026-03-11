using System;
using System.Threading.Tasks;

namespace TtsClient.TtsEngine
{
    public class DummyTtsEngine : ITtsEngine
    {
        public string EngineName => "Dummy Debug Engine";

        public async Task<byte[]> SynthesizeAsync(TtsRequest request)
        {
            // 1. 受け取った文字列をコンソールに表示
            Console.WriteLine($"[DummyTts] 読み上げテキスト: {request.Text}");
            Console.WriteLine($"[DummyTts] 使用ボイス: {request.Voice}");

            // 2. 数秒待機（ネットワーク遅延や生成時間をシミュレート）
            // 実際の API 待ち時間に近い 1.5秒 程度に設定
            await Task.Delay(1500);

            // 3. ダミーの結果を返す
            // 本来は MP3 等のバイナリですが、ここでは空の配列（または適当なバイト列）を返します
            Console.WriteLine("[DummyTts] 合成が完了しました。");

            return new byte[] { 0x49, 0x44, 0x33, 0x03, 0x00, }; // ダミーの MP3 ヘッダー風データ
        }
    }
}