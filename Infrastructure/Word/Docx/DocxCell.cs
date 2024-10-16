using System.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Word.Docx
{
    public class DocxCell : ICell
    {
        private Cell _cell;

        public DocxCell(Cell cell)
        {
            _cell = cell;
        }

        public string Value
        {
            get
            {
                // Get the text from the first paragraph in the cell
                return _cell.Paragraphs.FirstOrDefault()?.Text;
            }
            set
            {
                var paragraph = _cell.Paragraphs.FirstOrDefault();
                if (paragraph != null)
                {
                    try
                    {
                        // Clear the old text
                        _cell.ReplaceText(paragraph.Text, "");
                    }
                    catch
                    {
                        // Handle any exceptions if necessary (optional)
                    }
                    // Insert the new text
                    _cell.Paragraphs.FirstOrDefault()?.InsertText(value);
                }
                else
                {
                    // If no paragraph exists, create a new one with the given value
                    _cell.InsertParagraph(value);
                }
            }
        }

        public void AlignRight()
        {
            // Set the alignment of the first paragraph to the right
            var paragraph = _cell.Paragraphs.FirstOrDefault();
            if (paragraph != null)
            {
                paragraph.Alignment = Alignment.right;
            }
        }
    }
}
