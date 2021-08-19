using DiscoverParkTest.Models;
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
        private CustomerDTO customer;

        public CheckInPage()
        {
            InitializeComponent();
        }

        public CheckInPage(CustomerDTO customer)
        {
            InitializeComponent();

            this.customer = customer;
        }
    }
}