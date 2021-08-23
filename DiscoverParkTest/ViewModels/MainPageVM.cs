using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using DiscoverParkTest.Models;
using DiscoverParkTest.Views;
using DiscoverParkTest.ViewModels.Utils;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using DiscoverParkTest.Services;

namespace DiscoverParkTest.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
        
        // view list customers source
        public ObservableCollection<CustomerDTO> Customers { get; set; }
        private CustomerDTO customer { get; set; }
        private ResponseMessage message = new ResponseMessage();
        private ShowComponent indicator = new ShowComponent();
        private QueryCustomer queryCustomer = new QueryCustomer();
        private bool parkCodeError;
        private bool arrivingDateError;

        public event PropertyChangedEventHandler PropertyChanged;
        IApiConsume _apiConsume = DependencyService.Get<IApiConsume>();

        public MainPageVM()
        {
            Customers = new ObservableCollection<CustomerDTO>();
            SearchAndValidation = new Command(SearchCustomerAction);
        }

        // button command
        public Command SearchAndValidation { get; set; }

        // list view selected customer
        public CustomerDTO Customer
        {
            get => customer;
            set
            {
                // only assign value to customer when they are different
                // then navigate to Check In page
                if (customer != value)
                {
                    customer = value;
                    _ = Application.Current.MainPage.Navigation.PushAsync(new CheckInPage(value));
                }
            }
        }

        // main page query model
        public QueryCustomer QueryCustomer
        {
            get => queryCustomer;
            set => queryCustomer = value;
        }


        // control park code error message
        public bool ParkCodeError
        {
            get => parkCodeError;
            set
            {
                parkCodeError = value;
                OnPropertychanged();
            }
        }

        // control arriving date error message
        public bool ArrivingDateError
        {
            get => arrivingDateError;
            set
            {
                arrivingDateError = value;
                OnPropertychanged();
            }
        }

        // control retrived empty customer list message
        public ResponseMessage Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertychanged();
            }
        }

        // control of loading indicator
        public ShowComponent Indicator
        {
            get => indicator;
            set
            {
                indicator = value;
                OnPropertychanged();
            }
        }

        // search button action
        public void SearchCustomerAction()
        {
            // reset and hide buttom error message, if there is one
            Message = new ResponseMessage();

            // logic layer validate input value
            // if not valid stop here
            if (!ValidateInputs())
            {
                Message = new ResponseMessage(StatusMessages.InvalidInputs, 30);
                return;
            }

            // check if internet connection is enabled, if not alert user
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Message = new ResponseMessage(StatusMessages.NoNetwork, 30);
                return;
            }

            // clear customers collection before query constomer from api
            Customers.Clear();

            // query customer collection from api
            GetCustomers();
        }

        public async void GetCustomers()
        {
            // when pop check in page the on appearing execute, then the origianl customers collection need to be cleared
            Customers.Clear();

            // assemble url and end point
            var _url = $"{Uris.UrlConn}{Uris.NeedToBeSpoken}?ParkCode={queryCustomer.ParkCode}&Arriving={queryCustomer.ArrivingDate}";

            // quering api
            var response = await _apiConsume.Get(_url);

            // handle most cases of response status
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    Message = new ResponseMessage(StatusMessages.BadRequest, 30);
                    break;

                case System.Net.HttpStatusCode.OK:
                    string result = response.Content
                        .ReadAsStringAsync().Result;
                    ResponseToCustomers(result);
                    break;

                default:
                    break;
            }

            // hide loading indicator
            Indicator = new ShowComponent();


            // if it is not a bad request,
            // then check if customers list is empty
            // if it is empty, then switch on empty cusomter list control, otherwise hide bottom error message
            if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                Message = Customers.Count == 0 ?
                    new ResponseMessage(StatusMessages.EmptyCustomers, 30) : new ResponseMessage();
            }
        }

        // convert response content into customers collection
        private void ResponseToCustomers(string result)
        {
            // add retrieved customer to customers' collection
            try
            {
                var _customers = JsonConvert.DeserializeObject<ObservableCollection<CustomerDTO>>(result);
                foreach (CustomerDTO customer in _customers)
                {
                    Customers.Add(customer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        // validate from business layer before querying api
        private bool ValidateInputs()
        {
            ArrivingDateError = ValidateStringFormat.MatchDateFormat(queryCustomer.ArrivingDate);
            ParkCodeError = ValidateStringFormat.MatchParkCodeFormat(queryCustomer.ParkCode);
            return ArrivingDateError && ParkCodeError;
        }

        private void OnPropertychanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
