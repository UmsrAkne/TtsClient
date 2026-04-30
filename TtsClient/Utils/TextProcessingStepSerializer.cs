using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TtsClient.Texts;

namespace TtsClient.Utils
{
    public static class TextProcessingStepSerializer
    {
        private readonly static string DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        public static async Task SaveAsync(IEnumerable<TextProcessingStep> steps, string fileName)
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize(steps, options);
            var filePath = Path.Combine(DataDirectory, fileName);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<TextProcessingStep>> LoadAsync(string fileName)
        {
            var filePath = Path.Combine(DataDirectory, fileName);
            if (!File.Exists(filePath))
            {
                return new List<TextProcessingStep>();
            }

            var json = await File.ReadAllTextAsync(filePath);
            var steps = JsonSerializer.Deserialize<List<TextProcessingStep>>(json);
            return steps ?? new List<TextProcessingStep>();
        }
    }
}