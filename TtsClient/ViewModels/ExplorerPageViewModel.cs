using System.Collections.ObjectModel;
using Prism.Mvvm;
using TtsClient.Models;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ExplorerPageViewModel : BindableBase
    {
        public ObservableCollection<SpeechEntry> SpeechEntries { get; } = new ();
    }
}