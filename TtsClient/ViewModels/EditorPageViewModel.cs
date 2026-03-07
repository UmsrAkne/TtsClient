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

        public EditorPageViewModel(ITtsEngine ttsEngine)
        {
            this.ttsEngine = ttsEngine;
        }

        public AsyncRelayCommand SendRequestCommand => new (async () =>
        {
            var req = new TtsRequest
            {
                Text = "こんにちは、Google Cloud Text-to-Speechです。",
                LanguageCode = "ja-JP",
                Voice = "ja-JP-Wavenet-D",
                Gender = "MALE",
                AudioFormat = "MP3",
            };

            var byteArray = await ttsEngine.SynthesizeAsync(req);
            var fileName = $"{Guid.NewGuid()}.mp3";
            var path = Path.Combine(PathHelper.GetTtsAudioDirectoryPath(), fileName);

            await File.WriteAllBytesAsync(path, byteArray);
        });
    }
}