using System.Windows;
using Prism.Ioc;
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
        containerRegistry.RegisterSingleton<ITtsEngine, DummyTtsEngine>();
        containerRegistry.RegisterSingleton<ITtsEngine, GoogleTtsEngine>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Logger.Initialize(PathHelper.GetApplicationDirectory());

        base.OnStartup(e);
    }
}