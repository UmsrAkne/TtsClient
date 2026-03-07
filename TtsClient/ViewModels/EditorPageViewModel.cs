using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.TtsEngine;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {
        private readonly ITtsEngine ttsEngine;

        public EditorPageViewModel(ITtsEngine ttsEngine)
        {
            this.ttsEngine = ttsEngine;
        }

        public AsyncRelayCommand SendRequestCommand => new (async () =>
        {
            await Task.Delay(1500);
        });
    }
}