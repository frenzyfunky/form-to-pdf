using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormToPdf.Service
{
    public interface IPdfWriter
    {
        void WriteToPdf(string text, PdfWriterOptions options);
        MemoryStream Save();
    }
}
