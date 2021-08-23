using DiscoverParkTest.Models;
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
            if (mainPageVM.ParkCodeError && mainPageVM.ArrivingDateError && !mainPageVM.Message.IsVisible)
            {
                mainPageVM.Indicator = new ShowComponent(25);
            }
        }
    }
}
