using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using DiscoverParkTest.Models;
using DiscoverParkTest.Views;
using DiscoverParkTest.ViewModels.Utils;

namespace DiscoverParkTest.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<CustomerDTO> Customers { get; set; }
        private CustomerDTO customer { get; set; }
        private ValidateObj<string> parkCode = new ValidateObj<string>(ValidateType.parkCode, string.Empty);
        private ValidateObj<string> arrivingDate = new ValidateObj<string>(ValidateType.dateTime, string.Empty);

        public MainPageVM()
        {
            Customers = new ObservableCollection<CustomerDTO>();
            SearchAndValidation = new Command(GetCustomers);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomerDTO Customer
        {
            get => customer;
            set
            {
                if (customer != value)
                {
                    customer = value;
                    _ = Application.Current.MainPage.Navigation.PushAsync(new CheckInPage(value));
                }
            }
        }
        

        public ValidateObj<string> ParkCode
        {
            get => parkCode;
            set
            {
                parkCode.Value = ((string)value.Value).ToUpper();
                OnPropertychanged(nameof(ParkCode));
            }
        }
        public ValidateObj<string> ArrivingDate
        {
            get => arrivingDate;
            set
            {
                arrivingDate = value;
                OnPropertychanged(nameof(ArrivingDate));
            }
        }

        public Command SearchAndValidation { get; set; }
        public void GetCustomers()
        {
            
            if (ValidateInputs())
            {
                using (HttpClient client = new HttpClient())
                {
                    var uri = $"{Urls.UrlConn}{Urls.NeedToBeSpoken}?ParkCode={parkCode.Value}&Arriving={arrivingDate.Value}";
                    //$"https://discoverycodetest.azurewebsites.net/api/NPS/Customers?ParkCode={ParkCode}&Arriving={_arrvingDate}";

                    var response = client.GetAsync(uri).GetAwaiter().GetResult();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = response.Content
                            .ReadAsStringAsync()
                            .GetAwaiter()
                            .GetResult();

                        var _customers = JsonConvert.DeserializeObject<ObservableCollection<CustomerDTO>>(result);
                        Customers.Clear();
                        foreach (CustomerDTO customer in _customers)
                        {
                            Customers.Add(customer);
                        }
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Warning", "Server not available, please try later", "OK");
                    }
                }

            }
            else
            {
                // 
            }
        }

        private bool ValidateInputs()
        {
            return StringMatch.MatchDateFormat(arrivingDate.Value) && !string.IsNullOrWhiteSpace(parkCode.Value);
        }

        private void OnPropertychanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
