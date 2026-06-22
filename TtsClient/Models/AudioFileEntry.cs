using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TtsClient.Models
{
    public class AudioFileEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid SpeechEntryId { get; set; }

        /// <summary>
        /// 生成された音声ファイルへの相対パス
        /// </summary>
        [Required]
        public string AudioRelativePath { get; set; } = string.Empty;

        /// <summary>
        /// このファイル生成時に実際に送信したSSML、またはプロンプト
        /// </summary>
        [Required]
        public string ProcessedSsml { get; set; } = string.Empty;

        /// <summary>
        /// 生成待ち/完了/エラー（ファイルごとに状態を持つ）
        /// </summary>
        [Required]
        public SpeechEntryStatus Status { get; set; } = SpeechEntryStatus.Pending;

        /// <summary>
        /// 音声の生成日時
        /// </summary>
        [Required]
        public DateTimeOffset GeneratedAt { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 複数ある音声のうち、現在アプリで「本番用・お気に入り」として使うものかどうかのフラグ
        /// </summary>
        public bool IsActive { get; set; } = false;

        // ナビゲーションプロパティ：この音声ファイルの生成パラメーター（話者、速度など）
        public List<AudioMetadata> Metadata { get; set; } = new();
    }
}