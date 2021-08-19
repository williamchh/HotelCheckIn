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

        public CheckInPage(CustomerDTO customer)
        {
            InitializeComponent();
            checkInPageVM = (CheckInPageVM)Resources["vm"];
            checkInPageVM.Customer = customer;
        }
    }
}