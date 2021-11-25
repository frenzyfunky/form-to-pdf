using FormToPdf.Attributes;
using FormToPdf.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FormToPdf.Strategies
{
    public class WriteNameAddressStrategy : IWriteStrategy
    {
        private readonly IPdfWriter _pdfWriter;
        private readonly Dictionary<string, MethodInfo> methodDict = new Dictionary<string, MethodInfo>();

        public WriteNameAddressStrategy(IPdfWriter pdfWriter)
        {
            _pdfWriter = pdfWriter;
            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(m => m.GetCustomAttributes(typeof(FieldNameAttribute), false).Length > 0);
            foreach (var method in methods)
            {
                methodDict.Add(((FieldNameAttribute)method.GetCustomAttribute(typeof(FieldNameAttribute))).FieldName, method);
            }
        }
        public MemoryStream Execute(JObject formValues)
        {
            foreach (var value in formValues)
            {
                methodDict[value.Key].Invoke(this, new[] { value.Value.Value<string>() });
            }

            return _pdfWriter.Save();
        }

        [FieldName("name")]
        private void PlaceName(string value)
        {
            PdfWriterOptions options = new PdfWriterOptions();
            options.TextContainer = new PdfSharpCore.Drawing.XRect(200, 200, 200, 34);
            options.Font = new PdfSharpCore.Drawing.XFont("Arial", 24);
            options.Brush = PdfSharpCore.Drawing.XBrushes.Black;

            _pdfWriter.WriteToPdf(value, options);
        }

        [FieldName("address")]
        private void PlaceAddress(string value)
        {
            PdfWriterOptions options = new PdfWriterOptions();
            options.TextContainer = new PdfSharpCore.Drawing.XRect(200, 250, 200, 34);
            options.Font = new PdfSharpCore.Drawing.XFont("Arial", 24);
            options.Brush = PdfSharpCore.Drawing.XBrushes.Black;

            _pdfWriter.WriteToPdf(value, options);
        }
    }
}
