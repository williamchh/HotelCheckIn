using DiscoverParkTest.Models;
using DiscoverParkTest.ViewModels.Utils;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using DiscoverParkTest.Services;

namespace DiscoverParkTest.ViewModels
{
    public class CheckInPageVM : INotifyPropertyChanged
    {
        private CustomerDTO customer;
        private string email = string.Empty;
        private ResponseMessage message = new ResponseMessage();
        private ShowComponent indicator = new ShowComponent();

        IApiConsume _apiConsume = DependencyService.Get<IApiConsume>();
        public event PropertyChangedEventHandler PropertyChanged;


        public CheckInPageVM()
        {
            CommandCheckIn = new Command(CheckInWithEmail);
        }

        /// <summary>
        /// used for pass in selected customerDTO
        /// </summary>
        /// <param name="customer">selected customerDTO</param>
        public CheckInPageVM(CustomerDTO customer)
        {
            this.customer = customer;
            CommandCheckIn = new Command(CheckInWithEmail);
        }

        // submit button command
        public Command CommandCheckIn { get; set; }

        // check in customer 
        public CustomerDTO Customer
        {
            get => customer;
            set
            {
                customer = value;
                OnPropertychanged();
            }
        }

        // customer input email
        public string Email
        {
            get => email;
            set
            {
                email = value;

                OnPropertychanged();
            }
        }

        public bool InvalidEmail
        {
            get => Message.IsVisible;
            set => Message = value
                    ? new ResponseMessage()
                    : new ResponseMessage(StatusMessages.InvalidEmail, 50);
        }

        // control server response message
        public ResponseMessage Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertychanged();
            }
        }

        // control loading indicator
        public ShowComponent Indicator
        {
            get => indicator;
            set
            {
                indicator = value;
                OnPropertychanged();
            }
        }

        // check in button command's action
        public void CheckInWithEmail()
        {
            // business layer validation
            // if input email is empty 
            if (string.IsNullOrWhiteSpace(email))
            {
                Message = new ResponseMessage(StatusMessages.WhiteSpace, 50);
                return;
            }

            // validate email format
            // if email format is not correct then alert customer
            bool _match = ValidateStringFormat.MatchEmailFormat(email);
            if (!_match)
            {
                Message = new ResponseMessage(StatusMessages.InvalidEmail, 50);
                return;
            }

            // check if internet connection is enabled, if not alert user
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Message = new ResponseMessage(StatusMessages.NoNetwork, 50);
                return;
            }

            // post reservation id and customer email to api to check in
            PostCustomerCheckIn();

            // hide loading indicator
            Indicator = new ShowComponent();
        }

        private async void PostCustomerCheckIn()
        {
            // clear error message before quering api
            Message = new ResponseMessage();

            // assemble endpoint
            string _url = $"{Uris.UrlConn}{Uris.HaveSpokenTo}";

            string json = JsonConvert.SerializeObject(new HaveSpokenTo() { ResId = Customer.ReservationId, UserEmail = Email });
            StringContent _content = new StringContent(json, Encoding.UTF8, "application/json");

            // post checking json to api
            var response = await _apiConsume.Post(_url, _content);

            // handle most used response status
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.BadRequest:
                    Message = new ResponseMessage(StatusMessages.BadRequest, 50);
                    break;

                case System.Net.HttpStatusCode.NoContent:
                case System.Net.HttpStatusCode.OK:
                    _ = Application.Current.MainPage.Navigation.PopAsync();
                    break;

                default:
                    Message = new ResponseMessage(StatusMessages.BadRequest, 50);
                    break;
            }
        }
        private void OnPropertychanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
