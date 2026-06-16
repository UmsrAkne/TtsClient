using System.Collections.ObjectModel;
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

        public ExplorerPageViewModel(SpeechService speechService)
        {
            this.speechService = speechService;
        }

        public ObservableCollection<SpeechEntry> SpeechEntries { get; } = new ();

        public AsyncRelayCommand LoadFromDbAsyncCommand =>
            loadFromDbCommand ??= new AsyncRelayCommand(async () =>
            {
                SpeechEntries.Clear();
                SpeechEntries.AddRange(await speechService.GetHistoryAsync());
            });
    }
}