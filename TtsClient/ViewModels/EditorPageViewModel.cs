using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {

        public AsyncRelayCommand SendRequestCommand => new (async () =>
        {
            await Task.Delay(1500);
        });
    }
}