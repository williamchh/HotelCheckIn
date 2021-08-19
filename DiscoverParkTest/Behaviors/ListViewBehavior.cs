using DiscoverParkTest.Models;
using DiscoverParkTest.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DiscoverParkTest.Behaviors
{
    public class ListViewBehavior : Behavior<ListView>
    {
        ListView listView;
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            listView = bindable;
            listView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CustomerDTO customer = (CustomerDTO)listView.SelectedItem;
            Application.Current.MainPage.Navigation.PushAsync(new CheckInPage(customer));
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            listView.ItemSelected -= ListView_ItemSelected;

        }
    }
}
