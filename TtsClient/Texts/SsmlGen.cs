namespace TtsClient.Texts
{
    public class SsmlGen
    {
        public string SurroundProsody(string text)
        {
            return @"<speak> <google:style name=""lively""> <prosody pitch=""-2st"" rate=""90%"" > " + text + "</prosody> </google:style> </speak>";
        }

        public string GetSsmlTextWithTitleCall(string title, string text, string type = "azure")
        {
            if (type == "azure")
            {
                return @"<speak version=""1.0"" xmlns=""http://www.w3.org/2001/10/synthesis"" xmlns:mstts=""http://www.w3.org/2001/mstts"" xml:lang=""ja-JP"">"
                       + @"<voice name=""ja-JP-Masaru:DragonHDLatestNeural"">"
                       + @"<prosody rate=""-5%"">"
                       + @"タイトル、<emphasis level=""strong"">" + title + "</emphasis>。"
                       + text
                       + "</prosody>"
                       + "</voice>"
                       + "</speak>";
            }

            return @"<speak> <google:style name=""lively""> <prosody pitch=""-2st"" rate=""90%""> "
                   + @"タイトル、<emphasis level=""strong"">" + title + "</emphasis>。"
                   + text
                   + "</prosody> </google:style> </speak>";
        }
    }
}