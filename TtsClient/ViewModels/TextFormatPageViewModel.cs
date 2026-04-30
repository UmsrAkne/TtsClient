using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Mvvm;
using TtsClient.Texts;
using TtsClient.Utils;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TextFormatPageViewModel : BindableBase
    {
        private readonly EditorPageViewModel editorPageViewModel;
        private string originalText;
        private string processedText;
        private string currentPresetFileName = string.Empty;
        private ObservableCollection<string> presetFiles = new ();

        public TextFormatPageViewModel()
        {
            SetupDebugData();
        }

        public TextFormatPageViewModel(EditorPageViewModel editorPageViewModel)
        {
            this.editorPageViewModel = editorPageViewModel;

            if (Directory.Exists(TextProcessingStepSerializer.DataDirectory))
            {
                var files = Directory.GetFiles(TextProcessingStepSerializer.DataDirectory);
                PresetFiles = new ObservableCollection<string>(files.Select(Path.GetFileName));
            }

            SetupDebugData();
        }

        public ObservableCollection<TextProcessingStep> TextProcessingSteps { get; } = new ();

        public ObservableCollection<string> PresetFiles
        {
            get => presetFiles;
            set => SetProperty(ref presetFiles, value);
        }

        public string CurrentPresetFileName
        {
            get => currentPresetFileName;
            set => SetProperty(ref currentPresetFileName, value);
        }

        public string OriginalText { get => originalText; set => SetProperty(ref originalText, value); }

        public string ProcessedText { get => processedText; set => SetProperty(ref processedText, value); }

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

        public DelegateCommand StartTextProcessCommand => new DelegateCommand(() =>
        {
            var text = string.Empty;
            foreach (var step in TextProcessingSteps)
            {
                text += step.Execute(OriginalText);
            }

            ProcessedText = text;
        });

        public DelegateCommand CopyToEditorPanelCommand => new DelegateCommand(() =>
        {
            editorPageViewModel.PendingRequest.Text = ProcessedText;
        });

        public AsyncRelayCommand SaveStepsCommand => new (async () =>
        {
            var targetFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            if (!string.IsNullOrEmpty(CurrentPresetFileName))
            {
                targetFileName = CurrentPresetFileName;
            }

            await TextProcessingStepSerializer.SaveAsync(TextProcessingSteps, $"{targetFileName}.json");

            var files = Directory.GetFiles(TextProcessingStepSerializer.DataDirectory);
            PresetFiles = new ObservableCollection<string>(files.Select(Path.GetFileName));

            CurrentPresetFileName = $"{targetFileName}.json";
        });

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