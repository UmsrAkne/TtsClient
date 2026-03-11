using System;
using System.IO;

namespace TtsClient.Utils
{
    public static class PathHelper
    {
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
    }
}