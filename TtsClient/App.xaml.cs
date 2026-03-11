using System.Windows;
using Prism.Ioc;
using TtsClient.Databases;
using TtsClient.TtsEngine;
using TtsClient.Utils;
using TtsClient.ViewModels;
using TtsClient.Views;

namespace TtsClient;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<EditorPageViewModel>();
        containerRegistry.Register<ExplorerPageViewModel>();

        containerRegistry.RegisterSingleton<SpeechService>();
        containerRegistry.RegisterSingleton<ISpeechRepository, JsonSpeechRepository>();

        containerRegistry.RegisterSingleton<ITtsEngine, DummyTtsEngine>();
        containerRegistry.RegisterSingleton<ITtsEngine, GoogleTtsEngine>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Logger.Initialize(PathHelper.GetApplicationDirectory());

        base.OnStartup(e);
    }
}