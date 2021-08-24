namespace DiscoverParkTest.Models
{
    public class MainPageModel
    {
        /// <summary>
        /// Main page model
        /// </summary>
        public MainPageModel()
        {
            ParkCode = string.Empty;
            ArrivingDate = string.Empty;
            ParkCodeError = true;
            ArrivingDateError = true;
        }
        /// <summary>
        /// input park code
        /// </summary>
        public string ParkCode { get; set; }
        /// <summary>
        /// input arriving date
        /// </summary>
        public string ArrivingDate { get; set; }
        /// <summary>
        /// control of park code error alert
        /// </summary>
        public bool ParkCodeError { get; set; }
        /// <summary>
        /// control of arriving date error alert
        /// </summary>
        public bool ArrivingDateError { get; set; }
    }
}