using System;
using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.Texts;
using TtsClient.TtsEngine;
using TtsClient.Utils;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {
        private readonly TtsService ttsService;
        private readonly SsmlGen ssmlGen = new ();
        private TtsRequest pendingRequest = new ();

        public EditorPageViewModel(TtsService ttsService)
        {
            TtsService = ttsService;
            SetupDebugData();
        }

        public TtsRequest PendingRequest
        {
            get => pendingRequest;
            set => SetProperty(ref pendingRequest, value);
        }

        public TtsService TtsService { get; set; }

        public AsyncRelayCommand SendRequestCommand => new (async () =>
        {
            var req = new TtsRequest
            {
                Text = ssmlGen.SurroundProsody(PendingRequest.Text),
                Voice = "ja-JP-Wavenet-D",
            };

            var byteArray = await ttsService.SynthesizeAsync(req);
            var fileName = $"{DateTime.Now.ToString($"yyyyMMdd_HHmmss_fff")}.mp3";
            var path = Path.Combine(PathHelper.GetTtsAudioDirectoryPath(), fileName);

            await File.WriteAllBytesAsync(path, byteArray);
        });

        [Conditional("DEBUG")]
        private void SetupDebugData()
        {
            PendingRequest.Text = "グーグルクラウド Text To Speech のテストです。";
        }
    }
}