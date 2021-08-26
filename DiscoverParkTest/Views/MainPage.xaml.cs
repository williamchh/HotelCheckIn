using DiscoverParkTest.Models;
using DiscoverParkTest.Services;
using DiscoverParkTest.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiscoverParkTest
{
    public partial class MainPage : ContentPage
    {
        //private CustomerDTO customer;
        MainPageVM mainPageVM;
        public MainPage()
        {
            InitializeComponent();
            mainPageVM = (MainPageVM)Resources["vm"];
        }

        /// <summary>
        /// constructor for new Main Page with personal setting of locale
        /// </summary>
        /// <param name="locale">locale language text string</param>
        public MainPage(string locale)
        {
            // set locale text to App locale static string
            App.locale = locale;
            // get locale service
            var language = DependencyService.Get<ILocale>();
            // get desinated language json file
            language.GetLanguagePack(locale);

            InitializeComponent();
            mainPageVM = (MainPageVM)Resources["vm"];
        }

        /// <summary>
        /// when page shows up, load the following
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // only query customers when customers list's count is greater than zero
            // which means only checking page pops following code can be executed
            if (mainPageVM.Customers.Count > 0)
            {
                mainPageVM.GetCustomers();
            }

        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            // if all the inputs are correct, start loading indicator before call api
            if (mainPageVM.TriggerLoadingIndicator())
            {
                mainPageVM.Indicator = new ShowComponent(25);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ToolbarItem item = (ToolbarItem)sender;
            
            (Application.Current).MainPage = new NavigationPage(new MainPage(GetLocale(item.Text)));

        }
        private string GetLocale(string name)
        {
            switch (name)
            {
                case "Chinese":
                    return "zh-CN";
                case "English":
                default:
                    return "en-US";
            }
        }
    }
}
