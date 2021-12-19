using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Admin.Models
{
    public class HomeViewModel
    {
        public List<Pdf> Pdfs { get; set; } = new List<Pdf>();

        public class Pdf
        {
            public string Preview { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
