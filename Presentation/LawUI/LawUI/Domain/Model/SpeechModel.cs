namespace LawUI.Domain.Model
{
    public class SpeechModel
    {
        public string Transcription { get; set; } = string.Empty;
        public bool IsRecording { get; set; }
        public string LastTranscription { get; set; } = string.Empty;
        public bool IsRecognizingSpeech { get; set; } = false;
    }

}
