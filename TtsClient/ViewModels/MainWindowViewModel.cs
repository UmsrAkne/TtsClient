using Prism.Mvvm;
using TtsClient.Utils;

namespace TtsClient.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new ();

    public string Title => appVersionInfo.Title;

    public EditorPageViewModel EditorPageViewModel { get; } = new ();

    public ExplorerPageViewModel ExplorerPageViewModel { get; } = new ();
}