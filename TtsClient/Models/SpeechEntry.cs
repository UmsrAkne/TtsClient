using System;

namespace TtsClient.Models
{
    /// <summary>
    /// テキストと音声のセットを表すクラスです。
    /// </summary>
    public class SpeechEntry
    {
        /// <summary>
        /// ユーザーが入力した「生のテキスト」
        /// </summary>
        public string RawText { get; set; } = string.Empty;

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
    }
}