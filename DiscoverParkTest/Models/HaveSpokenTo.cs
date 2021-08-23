

namespace DiscoverParkTest.Models
{
    /// <summary>
    /// CheckIn Object
    /// After select customer information from list view,
    /// together with reservation id and customer email, post back to server
    /// </summary>
    public class HaveSpokenTo
    {
        public string ResId { get; set; }
        public string UserEmail { get; set; }
    }
}
