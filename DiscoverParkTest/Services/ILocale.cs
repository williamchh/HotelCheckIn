using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscoverParkTest.Services
{
    interface ILocale
    {

        Task<Dictionary<string, Dictionary<string, string>>> TextAsync(string locale);
        Dictionary<string, Dictionary<string, string>> GetText();
        void GetLanguagePack(string locale);
        void GetLanguagePack();
    }
}
