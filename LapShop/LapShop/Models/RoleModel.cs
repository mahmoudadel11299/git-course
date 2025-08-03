using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Models
{
    public class RoleModel
    {
        [ValidateNever] 
        public string? id { get; set; }
        [Required(ErrorMessage ="Enter a Name")]
        public string name { get; set; }
    }
}
