using DiscoverParkTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DiscoverParkTest.ViewModels
{
    public class CheckInPageVM : INotifyPropertyChanged
    {
        private CustomerDTO customer;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
