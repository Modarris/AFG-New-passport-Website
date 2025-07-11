﻿using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models.UserAccount
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
