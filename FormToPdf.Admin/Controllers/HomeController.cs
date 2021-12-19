using FormToPdf.Admin.Models;
using FormToPdf.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace FormToPdf.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _applicationContext;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            HomeViewModel model = new HomeViewModel();
            var pdfs = await _applicationContext.Pdfs.ToListAsync();

            foreach (var pdf in pdfs)
            {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(pdf.FilePath);
                var bmpStream = doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Bitmap);
                bmpStream.Position = 0;

                var bmp = Bitmap.FromStream(bmpStream);

                var memoryStream = new MemoryStream();
                bmp.Save(memoryStream, ImageFormat.Jpeg);
                byte[] byteImage = memoryStream.ToArray();

                var sigBase64 = Convert.ToBase64String(byteImage);

                model.Pdfs.Add(new HomeViewModel.Pdf
                {
                    Id = pdf.Id,
                    Name = pdf.Name,
                    Preview = sigBase64
                });

                await memoryStream.DisposeAsync();
                await bmpStream.DisposeAsync();
            }


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
