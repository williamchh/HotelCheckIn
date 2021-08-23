using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DiscoverParkTest.ViewModels.Utils
{
    public class ValidateStringFormat
    {
        /// <summary>
        /// check if the input date string is correct date string format for querying purpose
        /// year number should large than 2020
        /// month only between 01 - 12
        /// day only between 01-31
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool MatchDateFormat(string date)
        {
            Regex regex = new Regex(@"^[20]{2}[2-9]{1}[0-9]{1}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$");
            Match match = regex.Match(date);
            return match.Success;
        }

        /// <summary>
        /// Check if input park code is correct format, assume park code is four characters
        /// park code may include numbers
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        public static bool MatchParkCodeFormat(string parkCode)
        {
            if (string.IsNullOrWhiteSpace(parkCode))
                return false;

            Regex regex = new Regex(@"^[a-zA-Z0-9]{4}$");
            Match match = regex.Match(parkCode);
            return match.Success;
        }

        /// <summary>
        /// check if input email string is correct format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool MatchEmailFormat(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }
}
