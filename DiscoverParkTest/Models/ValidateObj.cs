using DiscoverParkTest.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscoverParkTest.Models
{
    public class ValidateObj<T>
    {
        public string ValidationMessage { get; set; } = string.Empty;
        public bool ValidateStatus { get; set; } = true;
        public ValidateType ValidateType { get; set; }
        public T Value { get; set; }

        public ValidateObj(ValidateType validateType, T value)
        {
            Value = value;
            ValidateType = validateType;
            Check(value);
        }


        private void Check(T value)
        {
            if (value == null)
            {
                ValidateStatus = false;
                ValidationMessage = "Error, CAN NOT BE NULL";
                return;
            }

            switch (ValidateType)
            {
                case ValidateType.dateTime:
                    if (!StringMatch.MatchDateFormat(value as string))
                    {
                        ValidateStatus = false;
                        ValidationMessage = "Invalid Date String";
                    }
                    break;
                case ValidateType.email:
                    if (!StringMatch.MatchEmailFormat(value as string))
                    {
                        ValidateStatus = false;
                        ValidationMessage = "Invalid Email";
                    }
                    break;
                case ValidateType.parkCode:
                    if (string.IsNullOrWhiteSpace(value as string))
                    {
                        ValidateStatus = false;
                        ValidationMessage = "Error, CAN NOT BE NULL";
                    }
                    break;
                default:
                    ValidateStatus = false;
                    ValidationMessage = "Unknown Error";
                    break;
            }
        }


    }
}
