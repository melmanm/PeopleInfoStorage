using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PeopleInfoStorage.Core.Extension
{
    public static class StringExtensions
    {
        

        public static void GetErrorIfEmpty(this String value, ref string result)
        {
            if (String.IsNullOrEmpty(value))
            {
                result = "Value can not be empty.";
            }
        }

        public static void GetErrorIfNotPostalCode(this String value, ref string result)
        {
            if (String.IsNullOrEmpty(value))
            {
                result = "Value can not be empty.";
                return;
            }

            var postalCodePattern = @"[0-9]{2}\-[0-9]{3}";
            var regex = new Regex(postalCodePattern);
            var match = regex.Match(value);
            if (!match.Success)
            {
                result = "Postal code is invalid. Valid pattern is 00-000.";
            }
        }

        public static void GetErrorIfNotPhoneNumber(this String value, ref string result)
        {
            if (String.IsNullOrEmpty(value))
            {
                result = "Value can not be empty.";
                return;
            }
            var postalCodePattern = @"[0-9]{9}";
            var regex = new Regex(postalCodePattern);
            var match = regex.Match(value);
            if (!match.Success)
            {
                result = "Phone number is invalid. Valid phone number contains 9 digits without spaces.";
            }
        }
    }
}
