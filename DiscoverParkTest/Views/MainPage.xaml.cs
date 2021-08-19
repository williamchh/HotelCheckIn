using DiscoverParkTest.Models;
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
        private CustomerDTO customer;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(CustomerDTO customer)
        {
            InitializeComponent();

            this.customer = customer;
        }
    }
}
