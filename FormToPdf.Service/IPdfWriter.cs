using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormToPdf.Service
{
    public interface IPdfWriter
    {
        void WriteToPdf(string text, string path, PdfWriterOptions options);
        MemoryStream Save();
    }
}
