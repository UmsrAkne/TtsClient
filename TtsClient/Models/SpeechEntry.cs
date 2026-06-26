using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prism.Mvvm;

namespace TtsClient.Models
{
    /// <summary>
    /// テキストと音声のセットを表すクラスです。
    /// </summary>
    public class SpeechEntry : BindableBase
    {
        private AudioFileEntry selectedAudio;

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
        /// UI表示用の文章のタイトル
        /// </summary>
        [Required]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 生成日時（ファイルの作成日とは別）
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual List<AudioFileEntry> AudioFiles { get; set; } = new ();

        [NotMapped]
        public AudioFileEntry SelectedAudio
        {
            get => selectedAudio;
            set => SetProperty(ref selectedAudio, value);
        }
    }
}