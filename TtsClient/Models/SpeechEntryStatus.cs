namespace TtsClient.Models
{
    /// <summary>
    /// ステータス種別 / Status kind for SpeechEntry.
    /// </summary>
    public enum SpeechEntryStatus
    {
        /// <summary>
        /// 生成待ち / Waiting to be generated.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 完了 / Successfully generated.
        /// </summary>
        Completed = 1,

        /// <summary>
        /// エラー / Failed to generate.
        /// </summary>
        Error = 2,
    }
}