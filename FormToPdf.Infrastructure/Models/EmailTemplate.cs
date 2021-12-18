using System;
using System.Collections.Generic;
using System.Text;

namespace FormToPdf.Infrastructure.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
    }
}
