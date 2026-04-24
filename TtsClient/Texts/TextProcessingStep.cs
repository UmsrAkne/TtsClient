using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace TtsClient.Texts
{
    public class TextProcessingStep : BindableBase
    {
        private string caption = string.Empty;
        private bool isSingleLine;
        private string extractionPattern = string.Empty;

        public string Caption { get => caption; set => SetProperty(ref caption, value); }

        public string ExtractionPattern
        {
            get => extractionPattern;
            set => SetProperty(ref extractionPattern, value);
        }

        public bool IsSingleLine
        {
            get => isSingleLine;
            set => SetProperty(ref isSingleLine, value);
        }

        public ObservableCollection<ReplacementRule> ReplacementRules { get; set; } = new ();

        public string Execute(string text)
        {
            var options = IsSingleLine ? RegexOptions.Singleline : RegexOptions.None;
            var matches = Regex.Matches(text, ExtractionPattern, options);

            // まず抽出。複数引っかる場合は改行つき結合。
            var builder = new StringBuilder();
            foreach (Match match in matches)
            {
                builder.AppendLine(match.Value);
            }

            var result = builder.ToString();

            // 抽出したテキストに対して置き換え処理。フォーマットの仕上げ用途を想定。
            foreach (var regexValue in ReplacementRules)
            {
                result = Regex.Replace(result, regexValue.Pattern, regexValue.Replacement);
            }

            return result;
        }
    }
}