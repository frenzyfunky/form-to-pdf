﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormToPdf.Admin.Models
{
    public class RegisterFormModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This is required field")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This is required field")]
        public string Password { get; set; }

        public List<IdentityError> IdentityErrors { get; set; } = new List<IdentityError>();
    }
}
