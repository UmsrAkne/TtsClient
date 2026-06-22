using System;
using System.ComponentModel.DataAnnotations;

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
        /// 生成日時（ファイルの作成日とは別）
        /// </summary>
        [Required]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}