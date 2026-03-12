using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.Models;
using TtsClient.Utils;

namespace TtsClient.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly SpeechService speechService;
    private readonly AppVersionInfo appVersionInfo = new ();

    public MainWindowViewModel()
    {
        SetupDebugData();
    }

    public MainWindowViewModel(
        EditorPageViewModel editorPageViewModel,
        ExplorerPageViewModel explorerPageViewModel,
        SpeechService speechService)
    {
        Logger.Log("MainWindowViewModel constructor executed.");

        this.speechService = speechService;
        EditorPageViewModel = editorPageViewModel;
        ExplorerPageViewModel = explorerPageViewModel;
        SetupDebugData();

        _ = LoadSpeechesAsync();
    }

    public string Title => appVersionInfo.Title;

    public EditorPageViewModel EditorPageViewModel { get; }

    public ExplorerPageViewModel ExplorerPageViewModel { get; }

    public TextFormatPageViewModel TextFormatPageViewModel { get; } = new ();

    private async Task LoadSpeechesAsync()
    {
        ExplorerPageViewModel.SpeechEntries.Clear();

        try
        {
            var data = await speechService.GetHistoryAsync();
            foreach (var item in data)
            {
                ExplorerPageViewModel.SpeechEntries.Add(item);
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.ToString());
        }
    }

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