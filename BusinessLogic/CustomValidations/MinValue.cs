using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.CustomValidations
{
    internal class MinValueAttribute : ValidationAttribute
    {
        private readonly double _minValue;

        public MinValueAttribute(double minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (Convert.ToDouble(value) < _minValue)
            {
                return new ValidationResult($"{ErrorMessage}   '{(validationContext.ObjectInstance as ShippingTypeDto)?.ShippingFactor}' is below minimum allowed value.");
            }

            return ValidationResult.Success;
        }
    }
}
