
namespace DiscoverParkTest.Models
{
    public class ShowComponent
    {
        /// <summary>
        /// component IsVisible false, hide the height to free up screen space
        /// </summary>
        public ShowComponent()
        {
            IsVisible = false;
            HeightRequest = 0;
        }

        /// <summary>
        /// turn on IsVisible and give component a height
        /// </summary>
        /// <param name="heightRequest">component request height</param>
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
