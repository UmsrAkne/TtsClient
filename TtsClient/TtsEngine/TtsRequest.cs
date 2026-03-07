using Prism.Mvvm;

namespace TtsClient.TtsEngine
{
    public class TtsRequest : BindableBase
    {
        private string text;
        private string voice;
        private string audioFormat = "MP3";

        public string Text { get => text; set => SetProperty(ref text, value); }

        public string LanguageCode { get; set; } = "ja-JP";

        public string Voice { get => voice; set => SetProperty(ref voice, value); }

        public string Gender { get; set; } = "MALE";

        public string AudioFormat { get => audioFormat; set => SetProperty(ref audioFormat, value); }
    }
}