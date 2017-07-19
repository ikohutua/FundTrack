using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundTrack.Infrastructure.Attributes
{
    public class MinValueAttribute: ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            this._minValue = minValue;
            ErrorMessage = "Значення не повинно бути менше за "+ minValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value >= this._minValue;            
        }


    }
}
