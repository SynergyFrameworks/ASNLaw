using System.Drawing;

namespace Excel
{
    public interface IFont
    {
        string Name { get; set; }
        int Size { get; set; }
        FontStyle FontStyle { get; set; }
        Color Color { get; set; }
    }
}
