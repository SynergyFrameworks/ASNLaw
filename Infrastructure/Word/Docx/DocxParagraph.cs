using Xceed.Document.NET;

namespace Word
{
    public class DocxParagraph : IParagraph
    {
        private Paragraph _paragraph;

        public DocxParagraph(Paragraph paragraph)
        {
            _paragraph = paragraph;
        }

        public string Text
        {
            get { return _paragraph.Text; }
            set { _paragraph.ReplaceText(_paragraph.Text, value); }
        }

        public void AlignLeft()
        {
            _paragraph.Alignment = Alignment.left;
        }

        public void AlignRight()
        {
            _paragraph.Alignment = Alignment.right;
        }

        public void AlignCenter()
        {
            _paragraph.Alignment = Alignment.center;
        }

        public void Bold()
        {
            _paragraph.Bold();
        }

        public void Italic()
        {
            _paragraph.Italic();
        }

        // Add other methods to expose more functionality as needed
    }
}
