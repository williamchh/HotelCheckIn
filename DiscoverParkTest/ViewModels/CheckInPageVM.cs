using DiscoverParkTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DiscoverParkTest.ViewModels
{
    public class CheckInPageVM : INotifyPropertyChanged
    {
        private CustomerDTO customer;

        public event PropertyChangedEventHandler PropertyChanged;
        public CheckInPageVM()
        {
            ReturnPreviousPage = new Command(ReturnPage);
        }

        public CheckInPageVM(CustomerDTO customer)
        {
            this.customer = customer;
            //ReturnPreviousPage = new Command<CustomerDTO>(value =>
            //{
            //    Application.Current.MainPage.Navigation.PopModalAsync();
            //});
            ReturnPreviousPage = new Command(ReturnPage);
        }

        public Command ReturnPreviousPage { get; set; }


        public CustomerDTO Customer 
        {
            get => customer;
            set
            {
                customer = value;
                OnPropertychanged(nameof(Customer));
            }
        }

        public void ReturnPage()
        {
            //Application.Current.MainPage.Navigation.PopModalAsync();
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnPropertychanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
