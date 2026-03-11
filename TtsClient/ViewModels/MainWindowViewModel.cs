using System.Diagnostics;
using Prism.Mvvm;
using TtsClient.Models;
using TtsClient.Utils;

namespace TtsClient.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new ();

    public MainWindowViewModel()
    {
        SetupDebugData();
    }

    public MainWindowViewModel(EditorPageViewModel editorPageViewModel)
    {
        Logger.Log("MainWindowViewModel constructor executed.");
        EditorPageViewModel = editorPageViewModel;
        SetupDebugData();
    }

    public string Title => appVersionInfo.Title;

    public EditorPageViewModel EditorPageViewModel { get; }

    public ExplorerPageViewModel ExplorerPageViewModel { get; } = new ();

    public TextFormatPageViewModel TextFormatPageViewModel { get; } = new ();

    [Conditional("DEBUG")]
    private void SetupDebugData()
    {
        for (var i = 0; i < 50; i++)
        {
            ExplorerPageViewModel.SpeechEntries.Add(new SpeechEntry()
            {
                AudioPath = $"test_{i:000}.mp3",
            });
        }
    }
}