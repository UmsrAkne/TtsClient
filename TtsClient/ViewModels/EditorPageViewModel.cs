using System;
using System.IO;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.TtsEngine;
using TtsClient.Utils;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {
        private readonly ITtsEngine ttsEngine;
        private TtsRequest pendingRequest = new ();

        public EditorPageViewModel(ITtsEngine ttsEngine)
        {
            this.ttsEngine = ttsEngine;
        }

        public TtsRequest PendingRequest
        {
            get => pendingRequest;
            set => SetProperty(ref pendingRequest, value);
        }

        public AsyncRelayCommand SendRequestCommand => new (async () =>
        {
            var req = new TtsRequest
            {
                Text = PendingRequest.Text,
                Voice = "ja-JP-Wavenet-D",
            };

            var byteArray = await ttsEngine.SynthesizeAsync(req);
            var fileName = $"{DateTime.Now.ToString($"yyyyMMdd_HHmmss_fff")}.mp3";
            var path = Path.Combine(PathHelper.GetTtsAudioDirectoryPath(), fileName);

            await File.WriteAllBytesAsync(path, byteArray);
        });
    }
}