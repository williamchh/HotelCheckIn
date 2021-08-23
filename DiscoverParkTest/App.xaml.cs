using DiscoverParkTest.Services;
using DiscoverParkTest.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscoverParkTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // register the quering api to the dependency
            DependencyService.Register<IApiConsume, ApiConsume>();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
