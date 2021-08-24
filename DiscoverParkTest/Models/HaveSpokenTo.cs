

namespace DiscoverParkTest.Models
{
    /// <summary>
    /// CheckIn Object
    /// After select customer information from list view,
    /// together with reservation id and customer email, post back to server
    /// </summary>
    public class HaveSpokenTo
    {
        /// <summary>
        /// selected customerDTO reservation ID
        /// </summary>
        public string ResId { get; set; }

        /// <summary>
        /// input user email
        /// </summary>
        public string UserEmail { get; set; }
    }
}
