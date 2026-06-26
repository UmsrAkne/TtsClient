using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.TtsEngine;

namespace TtsClient.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EditorPageViewModel : BindableBase
    {
        private readonly SpeechService speechService;
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
            if (string.IsNullOrWhiteSpace(PendingRequest?.Text))
            {
                return;
            }

            await speechService.ProcessSingleTextAsync(PendingRequest.Text, DateTime.Now, TtsService);
        });

        [Conditional("DEBUG")]
        private void SetupDebugData()
        {
            PendingRequest.Text = "グーグルクラウド Text To Speech のテストです。";
        }
    }
}