using DiscoverParkTest.Models;
using DiscoverParkTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscoverParkTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckInPage : ContentPage
    {
        CheckInPageVM checkInPageVM;

        public CheckInPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// receiving Customer object from main page
        /// </summary>
        /// <param name="customer">Check in customer</param>
        public CheckInPage(CustomerDTO customer)
        {
            InitializeComponent();
            checkInPageVM = (CheckInPageVM)Resources["vm"];
            checkInPageVM.Customer = customer;
        }

        private void BtnCheckIn_Clicked(object sender, EventArgs e)
        {
            // if all the inputs are correct, start loading indicator before call api
            if (!checkInPageVM.Message.IsVisible)
            {
                checkInPageVM.Indicator = new ShowComponent(25);
            }
        }
    }
}