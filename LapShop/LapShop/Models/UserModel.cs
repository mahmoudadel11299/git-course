using LapShop.Utlities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Models
{
    public class UserModel
    {

        [StringLength(100,ErrorMessage ="too short")]
        [Required]

        public string FirstName { get; set; }
        [StringLength(100, ErrorMessage = "too short")]
        [Required]

        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "too short")]
        [Remote(action: "EmailInUse",controller:"Users")]
        
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "too short")]

        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool rememberme { get; set; }

    }
}
