using BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.CustomValidations
{
    internal class MaxValueAttribute : ValidationAttribute
    {
        private readonly double _maxValue;

        public MaxValueAttribute(double maxValue)
        {
            _maxValue = maxValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (Convert.ToDouble(value) > _maxValue)
            {
                return new ValidationResult($"{ErrorMessage}   '{(validationContext.ObjectInstance as ShippingTypeDto)?.ShippingFactor}' is above maximum allowed value.");
            }

            return ValidationResult.Success;
        }
    }
}
