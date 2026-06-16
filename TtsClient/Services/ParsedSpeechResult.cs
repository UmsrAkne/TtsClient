using System.Collections.Generic;

namespace TtsClient.Services
{
    /// <summary>
    /// YAMLからパースされたデータの格納クラス
    /// </summary>
    public class ParsedSpeechResult
    {
        public string Title { get; set; } = string.Empty;

        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 必須項目以外の任意要素を格納するディクショナリ
        /// </summary>
        public Dictionary<string, string> Metadata { get; set; } = new ();
    }
}