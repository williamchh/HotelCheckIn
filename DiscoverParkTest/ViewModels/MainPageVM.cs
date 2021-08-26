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
using System.Collections.Generic;

namespace DiscoverParkTest.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {

        private CustomerDTO customer { get; set; }
        private ObservableCollection<CustomerDTO> customers;
        private ResponseMessage message = new ResponseMessage();
        private ShowComponent indicator = new ShowComponent();
        private MainPageModel mainPageModel = new MainPageModel();
        private Dictionary<string, string> languageText;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IApiConsume _apiConsume;
        private readonly ILocale _locale;

        /// <summary>
        /// Main page constructor
        /// create a new observable collection of customerDTO
        /// bind button command with action
        /// </summary>
        public MainPageVM()
        {
            Customers = new ObservableCollection<CustomerDTO>();
            SearchAndValidation = new Command(SearchCustomerAction);

            _apiConsume = DependencyService.Get<IApiConsume>();
            _locale = DependencyService.Get<ILocale>();
            LanguageText = _locale.GetText();
        }

        public Dictionary<string, string> LanguageText
        {
            get => languageText;
            set
            {
                languageText = value;
                OnPropertychanged();
            }
        }

        // button command
        public Command SearchAndValidation { get; set; }

        // check if there is any input errors
        // if not returen true
        internal bool TriggerLoadingIndicator()
        {
            return ParkCodeError && ArrivingDateError && !Message.IsVisible;
        }

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
        public MainPageModel MainPageModel
        {
            get => mainPageModel;
            set => mainPageModel = value;
        }

        // view list customers source
        public ObservableCollection<CustomerDTO> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertychanged();
            }
        }

        public string ParkCode
        {
            get => MainPageModel.ParkCode;
            set
            {
                MainPageModel.ParkCode = value.ToUpper();
                OnPropertychanged();
            }
        }


        // control park code error message
        public bool ParkCodeError
        {
            get => MainPageModel.ParkCodeError;
            set
            {
                MainPageModel.ParkCodeError = value;
                OnPropertychanged();
            }
        }

        // control arriving date error message
        public bool ArrivingDateError
        {
            get => MainPageModel.ArrivingDateError;
            set
            {
                MainPageModel.ArrivingDateError = value;
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

            // clear customers collection before query constomer from api
            Customers.Clear();

            // logic layer validate input value
            // if not valid stop here
            if (!ValidateInputs())
            {
                //Message = new ResponseMessage(StatusMessages.InvalidInputs, 30);
                Message = new ResponseMessage(LanguageText["errorFieldInputs"], 30);
                return;
            }

            // check if internet connection is enabled, if not alert user
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Message = new ResponseMessage(StatusMessages.NoNetwork, 30);
                return;
            }

            // query customer collection from api
            GetCustomers();
        }

        public async void GetCustomers()
        {
            // when pop check in page the on appearing execute, then the origianl customers collection need to be cleared
            Customers.Clear();

            // assemble url and end point
            var _url = $"{Uris.UrlConn}{Uris.NeedToBeSpoken}?ParkCode={mainPageModel.ParkCode}&Arriving={mainPageModel.ArrivingDate}";

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
                    Customers = ResponseToCustomers(result);
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
                    new ResponseMessage(LanguageText["noResultFound"], 30) : new ResponseMessage();
            }
        }

        // convert response content into customers collection
        private ObservableCollection<CustomerDTO> ResponseToCustomers(string result)
        {
            ObservableCollection<CustomerDTO> _customers = new ObservableCollection<CustomerDTO>();
            // add retrieved customer to customers' collection
            try
            {
                var _obCustomers = JsonConvert.DeserializeObject<ObservableCollection<CustomerDTO>>(result);
                foreach (CustomerDTO customer in _obCustomers)
                {
                    _customers.Add(customer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return _customers;
        }

        // validate from business layer before querying api
        private bool ValidateInputs()
        {
            ArrivingDateError = ValidateStringFormat.MatchDateFormat(mainPageModel.ArrivingDate);
            ParkCodeError = ValidateStringFormat.MatchParkCodeFormat(mainPageModel.ParkCode);
            return ArrivingDateError && ParkCodeError;
        }

        private void OnPropertychanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
