using FormToPdf.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Strategies
{
    public class WriteNameStrategy : IWriteStrategy
    {
        private readonly IPdfWriter _pdfWriter;

        public WriteNameStrategy(IPdfWriter pdfWriter)
        {
            _pdfWriter = pdfWriter;
        }
        public MemoryStream Execute(JObject formValues)
        {
            foreach (var value in formValues)
            {
                if (value.Key == "name")
                {
                    PdfWriterOptions options = new PdfWriterOptions();
                    options.TextContainer = new PdfSharpCore.Drawing.XRect(200, 200, 200, 34);
                    options.Font = new PdfSharpCore.Drawing.XFont("Arial", 24);
                    options.Brush = PdfSharpCore.Drawing.XBrushes.Black;

                    _pdfWriter.WriteToPdf(value.Value.Value<string>(), options);
                }
            }

            return _pdfWriter.Save();
        }
    }
}
