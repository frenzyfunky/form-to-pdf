using FormToPdf.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormToPdf.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pdf> Pdfs { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldToPdf> FieldToPdfs { get; set; }
    }
}
