using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Bridge2013
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void showOnMap(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            if (menu != null)
            {
                NavigationService.Navigate(new Uri("/MapPage.xaml?bridgeId="+menu.Tag, UriKind.Relative));
            }
            
        }

        private void navigateHere(object sender, RoutedEventArgs e)
        {

        }

        private void BridgeList_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UserControl c = BridgeList.SelectedItem as UserControl;
            if(c!=null)
                NavigationService.Navigate(new Uri("/BridgeInfo.xaml?bridgeId=" + c.Tag, UriKind.Relative));
        }
    }
}
