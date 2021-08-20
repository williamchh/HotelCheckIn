using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DiscoverParkTest.ViewModels.Utils
{
    public class StringMatch
    {
        public static bool MatchDateFormat(string date)
        {
            Regex regex = new Regex(@"^[202]{3}[0-9]{1}-[0-9]{2}-[0-9]{2}$");
            Match match = regex.Match(date);
            return match.Success;
        }

        public static bool MatchEmailFormat(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }
}
