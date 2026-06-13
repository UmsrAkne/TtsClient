using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Prism.DryIoc;
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
        containerRegistry.RegisterSingleton<EditorPageViewModel>();
        containerRegistry.Register<ExplorerPageViewModel>();
        containerRegistry.Register<TextFormatPageViewModel>();
        containerRegistry.Register<MyDbContext>();

        containerRegistry.RegisterSingleton<SpeechService>();
        containerRegistry.RegisterSingleton<ISpeechRepository, SpeechRepository>();

        containerRegistry.RegisterSingleton<ITtsEngine, DummyTtsEngine>();
        containerRegistry.RegisterSingleton<ITtsEngine, GoogleTtsEngine>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Logger.Initialize(PathHelper.GetApplicationDirectory());

        base.OnStartup(e);
    }

    protected override void OnInitialized()
    {
        // DIコンテナから MyDbContext を取り出して EnsureCreated を実行する
        using var context = Container.Resolve<MyDbContext>();

        #if DEBUG
        // デバッグ起動時のみ、毎回DBをリセットして初期化する
        context.Database.EnsureDeleted();
        #endif

        context.Database.EnsureCreated();
        base.OnInitialized();
    }
}