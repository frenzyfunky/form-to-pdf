using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FormToPdf.Service
{
    public class PdfWriter : IPdfWriter
    {
        string CERTIFICATE_PATH = $"{Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(PdfWriter)).Location), "certificate.pdf")}";

        private PdfDocument document = null;

        public void WriteToPdf(string text, PdfWriterOptions options)
        {
            LoadPdf();

            PdfPage page = document.Pages[0];
            XGraphics graph = XGraphics.FromPdfPage(page);

            //graph.TranslateTransform(page.Width / 2, page.Height / 2);
            //graph.RotateTransform(-90);
            //graph.TranslateTransform(-page.Width / 2, -page.Height / 2);

            var tf = new XTextFormatter(graph);
            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;

            tf.DrawString(text, options.Font, options.Brush, options.TextContainer);
            graph.Dispose();
        }
        public MemoryStream Save()
        {
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            return stream;
        }

        private void LoadPdf()
        {
            if (document is null)
            {
                document = PdfReader.Open(CERTIFICATE_PATH, PdfDocumentOpenMode.Modify);
            }
        }
    }
}
