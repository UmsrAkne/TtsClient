using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.Models;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ExplorerPageViewModel : BindableBase
    {
        private readonly SpeechService speechService;
        private AsyncRelayCommand loadFromDbCommand;
        private SpeechEntry currentEntry;

        public ExplorerPageViewModel(SpeechService speechService)
        {
            this.speechService = speechService;
        }

        public ObservableCollection<SpeechEntry> SpeechEntries { get; } = new ();

        public SpeechEntry CurrentEntry { get => currentEntry; set => SetProperty(ref currentEntry, value); }

        public AsyncRelayCommand LoadFromDbAsyncCommand =>
            loadFromDbCommand ??= new AsyncRelayCommand(async () =>
            {
                var l = SpeechEntries.MaxBy(s => s.CreatedAt);
                if (l != null)
                {
                    var additionRecords = await speechService.GetSpeechEntries(l.CreatedAt);
                    foreach (var additionRecord in additionRecords)
                    {
                        SpeechEntries.Insert(0, additionRecord);
                    }

                    return;
                }

                SpeechEntries.Clear();
                SpeechEntries.AddRange(await speechService.GetHistoryAsync());
            });
    }
}