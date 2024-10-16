using System.Drawing;
using Xceed.Document.NET;

namespace Word
{
    public class DocxFormatting : IFormatting
    {
        private Formatting _xceedFormatting;

        public DocxFormatting()
        {
            _xceedFormatting = new Formatting();
        }

        public DocxFormatting(Formatting xceedFormatting)
        {
            _xceedFormatting = xceedFormatting;
        }

        // Expose the underlying Xceed formatting
        public Formatting ToXceedFormatting()
        {
            return _xceedFormatting;
        }

        // Example formatting properties (add others as needed)
        public bool Bold
        {
            get { return _xceedFormatting.Bold ?? false; }
            set { _xceedFormatting.Bold = value; }
        }

        public bool Italic
        {
            get { return _xceedFormatting.Italic ?? false; }
            set { _xceedFormatting.Italic = value; }
        }

        public float Size
        {
            get { return (float)(_xceedFormatting.Size ?? 12f); }
            set { _xceedFormatting.Size = value; }
        }

        public Color FontColor
        {
            get { return (Color)_xceedFormatting.FontColor; }
            set { _xceedFormatting.FontColor = value; }
        }

        // Add other formatting options like underline, font name, etc. as needed
    }
}
