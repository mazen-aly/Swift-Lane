using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTOs
{
    public class ShipmentCarrierDto : BaseDto
    {
        [Display(Name = "Shipment Carrier Name")]
        [Required(ErrorMessage = "Shipment Carrier Name is a required field.", AllowEmptyStrings = false)]
        [MinLength(3, ErrorMessage = "Shipment Carrier Name can be at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "Shipment Carrier Name can be at most 100 characters.")]
        public string CarrierName { get; set; } = null!;
    }
}
