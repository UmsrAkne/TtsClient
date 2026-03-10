namespace TtsClient.Texts
{
    public class SsmlGen
    {
        public string SurroundProsody(string text)
        {
            return @"<speak> <google:style name=""lively""> <prosody pitch=""-2st"" rate=""90%"" > " + text + "</prosody> </google:style> </speak>";
        }
    }
}