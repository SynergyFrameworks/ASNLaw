﻿using System.Drawing;

namespace Excel.EPPlus
{
    public class EPPlusFont : IFont
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color Color { get; set; }
    }
}
