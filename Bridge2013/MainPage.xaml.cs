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
using Microsoft.Phone.Tasks;
using System.IO.IsolatedStorage;

namespace Bridge2013
{
    public partial class MainPage : PhoneApplicationPage
    {
        CustomMessageBox messageBox;
        public MainPage()
        {
            InitializeComponent();


            messageBox = new CustomMessageBox()
            {
                Message = "Приложение может использовать данные о вашем местоположении для отображения их на карте (эти данные никуда не передаются и не хранятся). Вы можете отключить это в настройках программы.",
                Caption = "Разрешить использовать сведения о местоположении?",
                LeftButtonContent = "Разрешить",
                RightButtonContent = "Запретить",
            };


            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        Settings.IsNavigationEnabled = true;
                        break;
                    case CustomMessageBoxResult.RightButton:
                    case CustomMessageBoxResult.None:
                        Settings.IsNavigationEnabled = false;
                        break;
                    default:
                        break;
                }
            };

            this.Loaded += (s, e) =>
            {
                if(!IsolatedStorageSettings.ApplicationSettings.Contains("IsNavigationEnabled"))
                    messageBox.Show();
            };
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            System.Diagnostics.Debug.WriteLine(1);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            System.Diagnostics.Debug.WriteLine(2);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            System.Diagnostics.Debug.WriteLine(3);
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

        private void PreferenceClicked(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PreferencesPage.xaml", UriKind.Relative));
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
    }
}
