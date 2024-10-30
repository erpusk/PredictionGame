using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTOs.LoginDTO
{
    public class LoginDto {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}