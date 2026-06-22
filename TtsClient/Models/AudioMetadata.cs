using System;
using System.ComponentModel.DataAnnotations;

namespace TtsClient.Models
{
    public class AudioMetadata
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AudioFileId { get; set; }

        /// <summary>
        /// パラメーター名（例: "SpeakerId", "Speed", "Emotion", "Pitch"）
        /// </summary>
        [Required]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 設定値
        /// </summary>
        [Required]
        public string Value { get; set; } = string.Empty;
    }
}