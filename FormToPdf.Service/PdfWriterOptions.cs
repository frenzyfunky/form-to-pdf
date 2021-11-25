using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormToPdf.Service
{
    public class PdfWriterOptions
    {
        public XRect TextContainer { get; set; }
        public XFont Font { get; set; }
        public XBrush Brush { get; set; }
    }
}
