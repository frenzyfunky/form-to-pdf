using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Admin.Models
{
    public class PdfDetailViewModel
    {
        public Infrastructure.Models.Pdf Pdf { get; set; }
        public string Base64Image { get; set; }
    }
}
