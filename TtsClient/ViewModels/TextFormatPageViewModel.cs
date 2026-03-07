using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Mvvm;
using TtsClient.Texts;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TextFormatPageViewModel : BindableBase
    {
        private string originalText;

        public TextFormatPageViewModel()
        {
            SetupDebugData();
        }

        public ObservableCollection<TextProcessingStep> TextProcessingSteps { get; } = new ();

        public string OriginalText { get => originalText; set => SetProperty(ref originalText, value); }

        [Conditional("DEBUG")]
        private void SetupDebugData()
        {
            for (var i = 0; i < 5; i++)
            {
                var tp = new TextProcessingStep
                {
                    Caption = $"Header_{i}",
                    ExtractionPattern = ".*",
                };

                for (int j = 0; j < i; j++)
                {
                    tp.ReplacementRules.Add(new ReplacementRule
                    {
                        Pattern = $"pattern_{j}",
                        Replacement = "replacement",
                    });
                }

                TextProcessingSteps.Add(tp);
            }
        }
    }
}