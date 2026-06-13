using System;

namespace TtsClient.Models
{
    /// <summary>
    /// テキストと音声のセットを表すクラスです。
    /// </summary>
    public class SpeechEntry
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 読み上げるテキスト
        /// </summary>
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 文章のタイトル
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 実際に送信したSSML（または最終プロンプト）
        /// </summary>
        public string ProcessedSsml { get; set; }

        /// <summary>
        /// 生成された音声ファイルへのパス（プロジェクトルートからの相対指定を考慮）
        /// </summary>
        public string AudioPath { get; set; }

        /// <summary>
        /// 生成待ち/完了/エラー
        /// </summary>
        public SpeechEntryStatus Status { get; set; } = SpeechEntryStatus.Pending;

        /// <summary>
        /// 生成日時（ファイルの作成日とは別）
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public SpeechMetadata SpeechMetadata { get; set; }
    }
}