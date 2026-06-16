using System;
using System.IO;

namespace TtsClient.Utils
{
    public static class PathHelper
    {
        /// <summary>
        /// オーディオディレクトリーのフルパスを取得します。
        /// </summary>
        /// <returns>オーディオディレクトリーのフルパス。</returns>
        public static string GetTtsAudioDirectoryPath()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audios");
            Directory.CreateDirectory(baseDir);
            return baseDir;
        }

        public static string GetApplicationDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetSpeechEntriesJsonFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "speech_entries.json");
        }
    }
}