
namespace DiscoverParkTest.Models
{
    public class ShowComponent
    {
        public ShowComponent()
        {
            IsVisible = false;
            HeightRequest = 0;
        }
        public ShowComponent(double heightRequest)
        {
            IsVisible = true;
            HeightRequest = heightRequest;
        }
        /// <summary>
        /// if Message needs to show up
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Message Height, 0 => shrink message size
        /// </summary>
        public double HeightRequest { get; set; }
    }
}
