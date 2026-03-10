using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TtsClient.TtsEngine
{
    public class GoogleTtsEngine : ITtsEngine
    {
        private readonly static HttpClient Client = new ();

        public string EngineName { get; } = "Google TTS";

        public async Task<byte[]> SynthesizeAsync(TtsRequest request)
        {
            var apiKey = Environment.GetEnvironmentVariable("GOOGLE_TTS_KEY");

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("環境変数 GOOGLE_TTS_KEY が設定されていません");
            }

            var url = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={apiKey}";

            var body = new
            {
                input = new { ssml = request.Text, },
                voice = new
                {
                    languageCode = request.LanguageCode,
                    name = request.Voice,
                    ssmlGender = request.Gender,
                },
                audioConfig = new
                {
                    audioEncoding = request.AudioFormat.ToUpper(),
                },
            };

            var json = JsonSerializer.Serialize(body);

            var response = await Client.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var text = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(text);
            var base64 = doc.RootElement.GetProperty("audioContent").GetString();

            return !string.IsNullOrWhiteSpace(base64)
                ? Convert.FromBase64String(base64)
                : Array.Empty<byte>();
        }
    }
}