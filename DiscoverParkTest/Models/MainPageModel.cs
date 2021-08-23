namespace DiscoverParkTest.Models
{
    public class MainPageModel
    {
        // Main Page model
        public MainPageModel()
        {
            ParkCode = string.Empty;
            ArrivingDate = string.Empty;
            ParkCodeError = true;
            ArrivingDateError = true;
        }
        public string ParkCode { get; set; }
        public string ArrivingDate { get; set; }
        public bool ParkCodeError { get; set; }
        public bool ArrivingDateError { get; set; }
    }
}