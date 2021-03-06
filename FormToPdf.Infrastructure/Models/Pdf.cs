using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FormToPdf.Infrastructure.Models
{
    public class Pdf
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public EmailTemplate Email { get; set; }
        public List<FieldToPdf> Fields { get; set; } = new List<FieldToPdf>();
    }
}
