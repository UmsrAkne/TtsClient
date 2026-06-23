using System;
using System.Windows.Media;

namespace TtsClient.Utils
{
    public static class AudioPlayerUtil
    {
        // MediaPlayerのインスタンスを保持（ガベージコレクションによる解放を防ぐため）
        private readonly static MediaPlayer MediaPlayer = new ();

        /// <summary>
        /// 指定された絶対パスのMP3ファイルを再生します。
        /// </summary>
        /// <param name="absolutePath">ファイルの絶対パス</param>
        public static void Play(string absolutePath)
        {
            try
            {
                // 一度現在のメディアを閉じる
                MediaPlayer.Close();

                // 絶対パスからUriオブジェクトを作成して開く
                MediaPlayer.Open(new Uri(absolutePath, UriKind.Absolute));

                // 再生
                MediaPlayer.Play();
            }
            catch (Exception ex)
            {
                // 必要に応じてログ出力やエラーハンドリング
                System.Diagnostics.Debug.WriteLine($"再生エラー: {ex.Message}");
            }
        }

        /// <summary>
        /// 再生中の音声を停止します。
        /// </summary>
        public static void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}