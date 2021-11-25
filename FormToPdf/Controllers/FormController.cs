using FormToPdf.Service;
using FormToPdf.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FormToPdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IWriteStrategy _writeStrategy;
        private readonly IConfiguration _configuration;
        private readonly IEmailCreator _emailCreator;
        private readonly IMailSender _mailSender;

        public FormController(IWriteStrategy writeStrategy, IConfiguration configuration, IEmailCreator emailCreator, IMailSender mailSender)
        {
            _writeStrategy = writeStrategy;
            _configuration = configuration;
            _emailCreator = emailCreator;
            _mailSender = mailSender;
        }

        [HttpPost]
        public IActionResult Post([FromBody] JObject formFields)
        {
            var stream = _writeStrategy.Execute(formFields);
            var fileName = $"{Guid.NewGuid()}.pdf";

            var path = Path.Combine(_configuration.GetValue<string>("filePath"), fileName);
            using (var fileStream = System.IO.File.Create(path))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            var body = _emailCreator.CreateFromTemplate(@$"{Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(PdfWriter)).Location), 
                _configuration.GetValue<string>("Email:Template"))}", formFields);

            try
            {
                _mailSender.Send(body, stream, fileName, "changethis@gmail.com");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            finally
            {
                stream.Dispose();
            }
        }
    }
}
