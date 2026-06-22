using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.Models;
using TtsClient.Services;
using TtsClient.Texts;
using TtsClient.TtsEngine;
using TtsClient.Utils;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {
        private readonly SsmlGen ssmlGen = new ();
        private readonly SpeechService speechService;
        private readonly YamlSpeechParser yamlSpeechParser = new ();
        private TtsRequest pendingRequest = new ();

        public EditorPageViewModel(TtsService ttsService, SpeechService speechService)
        {
            TtsService = ttsService;
            this.speechService = speechService;
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
            var results = yamlSpeechParser.Parse(PendingRequest.Text);
            var entry = new SpeechEntry()
            {
                Title = results.Title,
                Contents = results.Contents,
            };

            var titleCall = results.Metadata.FirstOrDefault(kv => kv.Key == "TitleCall").Value;
            titleCall = string.IsNullOrWhiteSpace(titleCall) ? string.Empty : titleCall;

            var req = new TtsRequest
            {
                Text = TtsService.GetSsmlTextWithTitleCall(entry.Title, entry.Contents, titleCall),
                Voice = "ja-JP-Wavenet-D",
            };

            var now = DateTime.Now;
            var dateDirName = now.ToString("yyyy-MM-dd");

            var byteArray = await TtsService.SynthesizeAsync(req);
            var fileName = $"{now.ToString($"yyyyMMdd_HHmmss_fff")}.mp3";
            var path = Path.Combine(PathHelper.AudioDirectoryName, dateDirName, fileName);
            var absoluteDirPath = Path.Combine(AppContext.BaseDirectory, PathHelper.AudioDirectoryName, dateDirName);
            Directory.CreateDirectory(absoluteDirPath);

            entry.ProcessedSsml = req.Text;
            entry.AudioRelativePath = path;
            await File.WriteAllBytesAsync(Path.Combine(absoluteDirPath, fileName), byteArray);
            await speechService.RegisterEntryAsync(entry);

            var metadataList = results.Metadata.Select(kv => new SpeechMetadata()
            {
                Key = kv.Key,
                Value = kv.Value,
                SpeechEntryId = entry.Id,
            });

            await speechService.RegisterMetadataRangeAsync(metadataList);
        });

        [Conditional("DEBUG")]
        private void SetupDebugData()
        {
            PendingRequest.Text = "グーグルクラウド Text To Speech のテストです。";
        }
    }
}