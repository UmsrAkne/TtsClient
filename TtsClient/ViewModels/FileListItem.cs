using System.IO;

namespace TtsClient.ViewModels
{
    public class FileListItem
    {
        public FileListItem(string fullPath)
        {
            FileInfo = new FileInfo(fullPath);
        }

        public FileInfo FileInfo { get; init; }
    }
}