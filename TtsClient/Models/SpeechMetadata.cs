using System.Collections.Generic;

namespace TtsClient.Models
{
    public class SpeechMetadata
    {
        public string Language { get; set; } = string.Empty;

        public string VoiceName { get; set; } = string.Empty;

        public Dictionary<string, string> Tags { get; set; } = new();
    }
}