using FormToPdf.Admin.Models;
using FormToPdf.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Admin.Controllers
{
    public class PdfController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;

        public PdfController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var pdf = await _applicationContext.Pdfs
                .Include(x => x.Fields)
                .ThenInclude(x => x.Field)
                .Include(x => x.Email)
                .FirstOrDefaultAsync(p => p.Id == id);

            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(pdf.FilePath);
            var bmpStream = doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Bitmap);
            bmpStream.Position = 0;

            var bmp = Bitmap.FromStream(bmpStream);

            var memoryStream = new MemoryStream();
            bmp.Save(memoryStream, ImageFormat.Jpeg);
            byte[] byteImage = memoryStream.ToArray();

            var sigBase64 = Convert.ToBase64String(byteImage);

            await memoryStream.DisposeAsync();
            await bmpStream.DisposeAsync();

            var model = new PdfDetailViewModel()
            {
                Pdf = pdf,
                Base64Image = sigBase64
            };

            return View(model);
        }
    }
}
