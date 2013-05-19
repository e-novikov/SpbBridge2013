using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Bridge2013
{
    public partial class PreferencesPage : PhoneApplicationPage
    {
        public PreferencesPage()
        {
            InitializeComponent();
            checkState();
        }

        private void ToggleClicked(object sender, RoutedEventArgs e)
        {
            Settings.IsNavigationEnabled = (bool)toggle.IsChecked;
            checkState();
        }

        private void checkState()
        {
            bool state = Settings.IsNavigationEnabled;
            if (state)
            {
                toggle.Content = "Включена";
            }
            else
                toggle.Content = "Выключена";

            toggle.IsChecked = state;
        }
    }
}