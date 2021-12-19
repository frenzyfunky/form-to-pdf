using FormToPdf.Infrastructure;
using FormToPdf.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Application.UseCases
{
    public class SendPdfAsMail : IUseCase<bool>
    {
        private readonly ApplicationContext _dbContext;
        private readonly IPdfWriter _pdfWriter;
        private readonly IEmailCreator _emailCreator;
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;

        public SendPdfAsMail(
                ApplicationContext dbContext,
                IPdfWriter pdfWriter,
                IEmailCreator emailCreator,
                IMailSender mailSender,
                IConfiguration configuration
            )
        {
            _dbContext = dbContext;
            _pdfWriter = pdfWriter;
            _emailCreator = emailCreator;
            _mailSender = mailSender;
            _configuration = configuration;
        }
        
        public async Task<bool> Execute(JObject formFields)
        {
            var pdfId = formFields["pdfId"].Value<int>();

            var pdf = await _dbContext.Pdfs
                .Include(x => x.Fields)
                .ThenInclude(x => x.Field)
                .Include(x => x.Email)
                .FirstOrDefaultAsync(p => p.Id == pdfId);

            foreach (var field in formFields)
            {
                var fieldValue = field.Value.Value<string>();
                var fieldName = field.Key;

                if (fieldName == "pdfId")
                {
                    continue;
                }

                var fieldOptions = pdf.Fields.FirstOrDefault(f => f.Field.FieldName == fieldName);

                PdfWriterOptions options = new PdfWriterOptions();
                options.TextContainer = new PdfSharpCore.Drawing.XRect(
                    fieldOptions.XPos,
                    fieldOptions.YPos,
                    fieldOptions.Width,
                    fieldOptions.Height
                );
                options.Font = new PdfSharpCore.Drawing.XFont(fieldOptions.FontName, fieldOptions.FontSize);
                options.Brush = new PdfSharpCore.Drawing.XSolidBrush(new PdfSharpCore.Drawing.XColor
                {
                    R = fieldOptions.FontColorR,
                    G = fieldOptions.FontColorG,
                    B = fieldOptions.FontColorB,
                });

                _pdfWriter.WriteToPdf(fieldValue, pdf.FilePath, options);
            }

            var stream = _pdfWriter.Save();

            var body = _emailCreator.CreateFromTemplate(pdf.Email.Email, formFields);

            try
            {
                _mailSender.Send(body, stream, $"{Guid.NewGuid()}.pdf", pdf.Email.Subject, _configuration.GetValue<string>("Email:To"));
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                stream.Dispose();
            }

            return true;
        }
    }
}
