using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace TtsClient.TtsEngine
{
    public class AzureTtsEngine : ITtsEngine
    {
        public string EngineName { get; } = "Azure TTS (SDK)";

        public async Task<byte[]> SynthesizeAsync(TtsRequest request)
        {
            // 1. 環境変数から設定を取得
            var apiKey = Environment.GetEnvironmentVariable("AZURE_TTS_KEY");
            const string region = "japaneast";

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("環境変数 AZURE_TTS_KEY または AZURE_TTS_REGION が設定されていません");
            }

            // 2. SDKの設定オブジェクトを作成
            var config = SpeechConfig.FromSubscription(apiKey, region);

            // 言語と声の指定（要求に合わせて設定）
            config.SpeechSynthesisLanguage = request.LanguageCode; // 例: "ja-JP"
            config.SpeechSynthesisVoiceName = request.Voice;       // 例: "ja-JP-NanamiNeural"
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Audio24Khz160KBitRateMonoMp3);

            // 3. シンセサイザーの初期化（nullを渡すとメモリ上のストリームに出力されます）
            using var synthesizer = new SpeechSynthesizer(config, null);

            // 4. 音声合成を実行
            using var result = await synthesizer.SpeakSsmlAsync(request.Text);

            // 5. 結果の判定とバイナリの取り出し
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                // これだけで byte[] が手に入ります！
                return result.AudioData;
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                throw new Exception($"Azure TTS 合成失敗: {cancellation.ErrorDetails}");
            }

            return Array.Empty<byte>();
        }

        public string GenerateSsmlText(string title, string text)
        {
            return @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xml:lang=""ja-JP"">"
                   + @"<voice name=""ja-JP-NaokiNeural"">"
                   + @"<prosody rate=""-5%"">"
                   + @"タイトル、<emphasis level=""strong"">" + title + "</emphasis>。"
                   + text
                   + "</prosody>"
                   + "</voice>"
                   + "</speak>";
        }
    }
}