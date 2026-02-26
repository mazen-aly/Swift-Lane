using BusinessLogic.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTOs
{
    public class ShippingTypeDto : BaseDto
    {
        [Display(Name = "Shipping Type")]
        [Required(ErrorMessage = "Shipping Type Name is a required field.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Shipping Type Name can be at least 3 characters.")]
        [MaxLength(30, ErrorMessage = "Shipping Type Name can be at most 30 characters.")]
        public string ShippingTypeName { get; set; } = null!;


        [Display(Name = "Shipping Cost Calculating Factor")]
        [Required(ErrorMessage = "Shipping Factor is a required field.")]
        [MinValue(1, ErrorMessage = "Shipping factor can be at least 1.")]
        [MaxValue(10, ErrorMessage = "Shipping factor can be at most 10.")]
        public double? ShippingFactor { get; set; }
    }
}
