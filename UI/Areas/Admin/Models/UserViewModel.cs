using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Admin.Models
{
    public class UserViewModel
    {
        [ValidateNever]
        public string Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleName { get; set; }
    }
}
