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

        //public MainPage(CustomerDTO customer)
        //{
        //    InitializeComponent();

        //    this.customer = customer;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (mainPageVM.Customers.Count > 0)
            {
                mainPageVM.GetCustomers();
            }

        }
    }
}
