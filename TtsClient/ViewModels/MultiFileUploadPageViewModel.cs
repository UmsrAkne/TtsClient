using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace TtsClient.ViewModels
{
    public class MultiFileUploadPageViewModel : BindableBase
    {
        private FileListItem selectedItem;
        private string text;

        public ObservableCollection<FileListItem> TextFiles { get; set; } = new ();

        public FileListItem SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        public string Text { get => text; set => SetProperty(ref text, value); }
    }
}