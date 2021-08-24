

namespace DiscoverParkTest.Models
{
    public class ResponseMessage : ShowComponent
    {
        /// <summary>
        /// By default alert message doesnot need to show up
        /// IsVisible is false, request height is zero to free up screen space
        /// </summary>
        public ResponseMessage()
        {
            IsVisible = false;
            Message = string.Empty;
            HeightRequest = 0;
        }

        /// <summary>
        /// show message lable, turn on IsVisible
        /// </summary>
        /// <param name="message">alert message, mostly is error message</param>
        /// <param name="heightRequest">required height</param>
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
