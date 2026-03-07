namespace TtsClient.TtsEngine
{
    public class TtsRequest
    {
        public string Text { get; set; }

        public string LanguageCode { get; set; }

        public string Voice { get; set; }

        public string Gender { get; set; }

        public string AudioFormat { get; set; } = "mp3";
    }
}