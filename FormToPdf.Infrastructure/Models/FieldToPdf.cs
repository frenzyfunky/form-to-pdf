using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FormToPdf.Infrastructure.Models
{
    public class FieldToPdf
    {
        public int Id { get; set; }

        [Required]
        public Field Field { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public byte FontColorR { get; set; }
        public byte FontColorG { get; set; }
        public byte FontColorB { get; set; }
    }
}
