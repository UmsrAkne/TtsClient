using System.Collections.ObjectModel;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.Models;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ExplorerPageViewModel : BindableBase
    {
        private readonly SpeechService speechService;

        public ExplorerPageViewModel(SpeechService speechService)
        {
            this.speechService = speechService;
        }

        public ObservableCollection<SpeechEntry> SpeechEntries { get; } = new ();
    }
}