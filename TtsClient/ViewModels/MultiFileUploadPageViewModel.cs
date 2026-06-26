using System.Collections.ObjectModel;
using System.IO;
using Prism.Mvvm;

namespace TtsClient.ViewModels
{
    public class MultiFileUploadPageViewModel : BindableBase
    {
        private FileListItem selectedItem;
        private string fileContent;

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