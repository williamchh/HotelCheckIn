namespace DiscoverParkTest.Models
{
    public class QueryCustomer
    {
        // Main Page model
        public QueryCustomer()
        {
            ParkCode = string.Empty;
            ArrivingDate = string.Empty;
        }
        public string ParkCode { get; set; }
        public string ArrivingDate { get; set; }
    }
}