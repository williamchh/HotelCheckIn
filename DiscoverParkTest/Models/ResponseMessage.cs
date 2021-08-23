

namespace DiscoverParkTest.Models
{
    public class ResponseMessage : ShowComponent
    {
        /// <summary>
        /// By default alert message doesnot need to show up
        /// </summary>
        public ResponseMessage()
        {
            IsVisible = false;
            Message = string.Empty;
            HeightRequest = 0;
        }

        public ResponseMessage(string message, double heightRequest)
        {
            IsVisible = true;
            Message = message;
            HeightRequest = heightRequest;
        }
        

        /// <summary>
        /// alert message
        /// </summary>
        public string Message { get; set; }

    }
}
