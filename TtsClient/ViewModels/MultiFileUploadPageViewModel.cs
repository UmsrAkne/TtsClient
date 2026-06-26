using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.Input;
using Prism.Mvvm;
using TtsClient.Databases;
using TtsClient.TtsEngine;

namespace TtsClient.ViewModels
{
    public class MultiFileUploadPageViewModel : BindableBase
    {
        private readonly SpeechService speechService;
        private readonly TtsService ttsService;
        private FileListItem selectedItem;
        private string fileContent;

        public MultiFileUploadPageViewModel(SpeechService speechService, TtsService ttsService)
        {
            this.speechService = speechService;
            this.ttsService = ttsService;
        }

        public ObservableCollection<FileListItem> TextFiles { get; set; } = new ();

        public FileListItem SelectedItem
        {
            get => selectedItem;
            set
            {
                if (SetProperty(ref selectedItem, value))
                {
                    // 同期的にファイルを読み込んでTextBox用のプロパティに入れる
                    UpdateFileContent(value);
                }
            }
        }

        public string FileContent { get => fileContent; set => SetProperty(ref fileContent, value); }

        public AsyncRelayCommand BulkProcessCommand => new (async () =>
        {
            foreach (var fileItem in TextFiles)
            {
                try
                {
                    // ファイルからテキストを読み込んで順番に処理
                    var text = await File.ReadAllTextAsync(fileItem.FileInfo.FullName);
                    await speechService.ProcessSingleTextAsync(text, DateTime.Now, ttsService);
                    Console.WriteLine($"Processed file: {fileItem.FileInfo.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file: {fileItem.FileInfo.Name}");
                }
            }
        });

        private void UpdateFileContent(FileListItem item)
        {
            if (item?.FileInfo is not { Exists: true, })
            {
                FileContent = string.Empty;
                return;
            }

            try
            {
                // 同期処理で読み込み。データは使い捨て。
                FileContent = File.ReadAllText(item.FileInfo.FullName);
            }
            catch (IOException ex)
            {
                // ファイルがロックされている場合などのエラーハンドリング
                FileContent = $"読み込みエラー:\n{ex.Message}";
            }
        }
    }
}