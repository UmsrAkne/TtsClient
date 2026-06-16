using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TtsClient.Services
{
    /// <summary>
    /// YAML文字列からSpeechEntry用データを抽出するクラス
    /// </summary>
    public class YamlSpeechParser
    {
        private readonly IDeserializer deserializer =
                new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        /// <summary>
        /// YAML文字列を解析し、必須要素と任意要素に分離します。
        /// </summary>
        /// <param name="yamlInput">解析するYAML文字列</param>
        /// <returns>必須要素と任意要素が分離された結果</returns>
        public ParsedSpeechResult Parse(string yamlInput)
        {
            if (string.IsNullOrWhiteSpace(yamlInput))
            {
                throw new ArgumentException("YAML入力が空です。", nameof(yamlInput));
            }

            var rawData = deserializer.Deserialize<Dictionary<string, object>>(yamlInput);

            if (rawData == null)
            {
                Console.WriteLine("YAMLのパースに失敗しました。正しいフォーマットか確認してください。");
                return new ParsedSpeechResult();
            }

            var result = new ParsedSpeechResult();

            foreach (var kvp in rawData)
            {
                // 大文字小文字を区別せずに必須項目をチェック
                var keyLower = kvp.Key.ToLowerInvariant();
                var stringValue = kvp.Value?.ToString() ?? string.Empty;

                if (keyLower == "title")
                {
                    result.Title = stringValue;
                }
                else if (keyLower == "contents")
                {
                    result.Contents = stringValue;
                }
                else
                {
                    result.Metadata[kvp.Key] = stringValue; // 任意のメタデータ
                }
            }

            // バリデーション（データ検証）
            if (string.IsNullOrWhiteSpace(result.Title))
            {
                Console.WriteLine("必須要素 'Title' が見つからないか、空です。");
                return new ParsedSpeechResult();
            }

            if (string.IsNullOrWhiteSpace(result.Contents))
            {
                Console.WriteLine("必須要素 'Contents' が見つからないか、空です。");
                return new ParsedSpeechResult();
            }

            return result;
        }
    }
}