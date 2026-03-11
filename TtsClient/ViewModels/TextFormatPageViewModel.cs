using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Commands;
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

        public DelegateCommand AddStepCommand => new (() =>
        {
            TextProcessingSteps.Add(new TextProcessingStep());
        });

        public DelegateCommand<TextProcessingStep> RemoveStepCommand => new (step =>
        {
            if (step == null)
            {
                return;
            }

            TextProcessingSteps.Remove(step);
        });

        public DelegateCommand<TextProcessingStep> AddReplacementRuleCommand => new (AddReplacementRule);

        public DelegateCommand<ReplacementRule> RemoveReplacementRuleCommand => new (RemoveReplacementRule);

        private void AddReplacementRule(TextProcessingStep param)
        {
            if (param == null)
            {
                throw new ArgumentException("param is null", nameof(param));
            }

            param.ReplacementRules.Add(new ReplacementRule());
        }

        private void RemoveReplacementRule(ReplacementRule rule)
        {
            if (rule == null)
            {
                return;
            }

            // Find the owning step and remove the rule
            var owner = TextProcessingSteps.FirstOrDefault(s => s.ReplacementRules.Contains(rule));
            owner?.ReplacementRules.Remove(rule);
        }

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