using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using DiscoverParkTest.Models;

namespace DiscoverParkTest.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
        public ObservableCollection<CustomerDTO> Customers { get; set; }
        private string parkCode;
        private DateTime arrivingDate;

        public MainPageVM()
        {
            Customers = new ObservableCollection<CustomerDTO>();
            SearchAndValidation = new Command(GetCustomers);
            arrivingDate = DateTime.Today;
            parkCode = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string ParkCode
        {
            get => parkCode;
            set
            {
                parkCode = value.ToUpper();
                OnPropertychanged(nameof(ParkCode));
            }
        }
        public DateTime ArrvingDate
        {
            get => arrivingDate;
            set
            {
                arrivingDate = value;
                OnPropertychanged(nameof(ArrvingDate));
            }
        }

        public Command SearchAndValidation { get; set; }
        public void GetCustomers()
        {
            
            if (ValidateInputs())
            {
                string _arrvingDate = arrivingDate.ToString("yyyy-MM-dd");

                using (HttpClient client = new HttpClient())
                {
                    var uri = $"https://discoverycodetest.azurewebsites.net/api/NPS/Customers?ParkCode={ParkCode}&Arriving=2020-12-02";

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

        private bool ValidateInputs() => parkCode.Trim() != "";

        private void OnPropertychanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
