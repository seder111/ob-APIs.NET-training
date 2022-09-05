﻿using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class UserLogin
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
