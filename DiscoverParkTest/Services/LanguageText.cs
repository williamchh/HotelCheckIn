using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DiscoverParkTest.Services
{
    public class LanguageText : ILocale
    {
        private Dictionary<string, Dictionary<string, string>> language;

        public LanguageText()
        {
            GetLanguagePack();
        }

        /// <summary>
        /// Get designated language json file with locale string
        /// </summary>
        /// <param name="locale">designated language string</param>
        public void GetLanguagePack(string locale)
        {
            language = TextAsync(App.locale).Result;
        }

        /// <summary>
        /// Get default language json file depends on device preference language
        /// </summary>
        public void GetLanguagePack()
        {
            string languageSet = App.locale;
            if (App.locale == string.Empty)
            {
                var language = CultureInfo.CurrentUICulture;
                languageSet = language.Name;
                if (language.DisplayName.Contains("English")) languageSet = "en-US";
            }

            Language = TextAsync(languageSet).Result;

        }

        public Dictionary<string, Dictionary<string, string>> Language
        {
            get => language;
            private set => language = value;
        }

        // return locale Dictionary
        public Dictionary<string, Dictionary<string, string>> GetText()
        {
            return Language;
        }

        /// <summary>
        /// load language json file on device with locale string
        /// </summary>
        /// <param name="locale">locale language string</param>
        /// <returns></returns>
        public async Task<Dictionary<string, Dictionary<string, string>>> TextAsync(string locale)
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync($"{locale}.json"))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("gb2312")))
                {
                    var fileContents = reader.ReadToEnd();

                    string result = Regex.Replace(fileContents, "\n|\r", string.Empty);

                    Dictionary<string, Dictionary<string, string>> language = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(result);
                    //Console.WriteLine(fileContents);
                    return language;
                }
            }
        }
    }
}
