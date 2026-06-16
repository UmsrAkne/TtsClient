using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TtsClient.Models
{
    /// <summary>
    /// テキストと音声のセットを表すクラスです。
    /// </summary>
    public class SpeechEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 読み上げるテキスト
        /// </summary>
        [Required]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// 文章のタイトル
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 実際に送信したSSML（または最終プロンプト）
        /// </summary>
        [Required]
        public string ProcessedSsml { get; set; } = string.Empty;

        /// <summary>
        /// 生成された音声ファイルへのパス（プロジェクトルートからの相対指定を考慮）
        /// </summary>
        [Required]
        public string AudioPath { get; set; } = string.Empty;

        /// <summary>
        /// 生成待ち/完了/エラー
        /// </summary>
        [Required]
        public SpeechEntryStatus Status { get; set; } = SpeechEntryStatus.Pending;

        /// <summary>
        /// 生成日時（ファイルの作成日とは別）
        /// </summary>
        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}