using DiscoverParkTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace DiscoverParkTest.ViewModels
{
    public class CheckInPageVM : INotifyPropertyChanged
    {
        private CustomerDTO customer;
        private string email = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        public CheckInPageVM()
        {
            CommandCheckIn = new Command(CheckInWithEmail);
        }

        public CheckInPageVM(CustomerDTO customer)
        {
            this.customer = customer;
            CommandCheckIn = new Command(CheckInWithEmail);
        }

        public Command CommandCheckIn { get; set; }

        public CustomerDTO Customer { get; set; }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertychanged(nameof(Email));
            }
        }

        public void CheckInWithEmail()
        {
            // if input email is empty 
            if (string.IsNullOrEmpty(email))
            {

            }

            // validate email format
            bool _match = Utils.StringMatch.MatchEmailFormat(email);

            // if email format is correct then post to server
            if (_match)
            {
                using (HttpClient client = new HttpClient())
                {

                }
                _ = Application.Current.MainPage.Navigation.PopAsync();
            }
            // if email format is not correct then alert customer
            else
            {

            }
        }

        private void OnPropertychanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
