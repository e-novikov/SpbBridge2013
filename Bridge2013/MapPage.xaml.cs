using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media;
using System.Device.Location;
using System.IO.IsolatedStorage;

namespace Bridge2013
{
    public partial class MapPage
    {
        List<PushpinBridgeViewModel> bridgeVM = new List<PushpinBridgeViewModel>();
        GeoCoordinateWatcher gcw;

        public MapPage()
        {
            InitializeComponent();
            LoadBridges();
        }

        private void ButtonZoomIn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            map.ZoomLevel++;
            System.Diagnostics.Debug.WriteLine(map.ZoomLevel);
        }

        private void ButtonZoomOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            map.ZoomLevel--;
        }

        private void LoadBridges()
        {
            BridgesInfo i = BridgesInfo.Instance;

            
            foreach (Bridge b in i.Bridges)
            {
                bridgeVM.Add(new PushpinBridgeViewModel()
                {
                    Position = new GeoCoordinate(b.Latitude, b.Longitude),
                    Tag = b.Description,
                    Content = "",
                    Id = b.Id,
                });
            }
            mapItemsControl.ItemsSource = bridgeVM;
        }

        private void locationPushpin_Tap(object sender, GestureEventArgs e)
        {
            Pushpin locationPushpin = sender as Pushpin;
            string content = (string)locationPushpin.Content;
            if (content.Equals(""))
            {
                locationPushpin.Content = locationPushpin.Tag;
            }
            else
            {
                locationPushpin.Content = "";
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string id;

            if (Settings.IsNavigationEnabled)
            {
                if (gcw == null)
                    gcw = new GeoCoordinateWatcher();

                gcw = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                gcw.Start();
                gcw.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(CoordinateWatcher_PositionChanged);
            }
            
            if (NavigationContext.QueryString.TryGetValue("bridgeId", out id))
            {
                int bridgeId = int.Parse(id);
                Bridge b = BridgesInfo.Instance.Bridges.First<Bridge>(item => item.Id == bridgeId);
                if (b == null)
                    return;

                foreach (var item in mapItemsControl.ItemsSource)
                {
                    PushpinBridgeViewModel vm = item as PushpinBridgeViewModel;
                    if (vm != null && vm.Id == bridgeId)
                    {
                        vm.ContentVisibility = System.Windows.Visibility.Visible;
                        map.Center = new GeoCoordinate(b.Latitude, b.Longitude);
                        vm.Content = vm.Tag;
                        break;
                    }
                }
            }
        }

        void CoordinateWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!e.Position.Location.IsUnknown)
            {
                CurrentPositionPin.Visibility = System.Windows.Visibility.Visible;
                CurrentPositionPin.Location = e.Position.Location;
            }
        }

        private void Button1Tap(object sender, EventArgs e)
        {
            if (!Settings.IsNavigationEnabled)
            {
                MessageBox.Show("Disabled");
                return;
            }

            switch (gcw.Status)
            {
                case GeoPositionStatus.Disabled:
                    MessageBox.Show("Disabled");
                    break;
                case GeoPositionStatus.NoData:
                    MessageBox.Show("NoData");
                    break;
            }
            if (!gcw.Position.Location.IsUnknown)
            {
                map.Center = gcw.Position.Location;
                CurrentPositionPin.Visibility = System.Windows.Visibility.Visible;
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (gcw != null)
                gcw.Stop();
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

    public enum GoogleTileTypes
    {
        Hybrid,
        Physical,
        Street,
        Satellite,
        WaterOverlay
    }

    public class GoogleTile : Microsoft.Phone.Controls.Maps.TileSource
    {
        private int _server;
        private char _mapmode;
        private GoogleTileTypes _tiletypes;

        public GoogleTileTypes TileTypes
        {
            get { return _tiletypes; }
            set
            {
                _tiletypes = value;
                MapMode = MapModeConverter(value);
            }
        }

        public char MapMode
        {
            get { return _mapmode; }
            set { _mapmode = value; }
        }

        public int Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public GoogleTile()
        {
            UriFormat = @"http://mt{0}.google.com/vt/lyrs={1}&z={2}&x={3}&y={4}&hl=ru";
            Server = 0;
        }

        public override Uri GetUri(int x, int y, int zoomLevel)
        {
            if (zoomLevel > 0)
            {
                var Url = string.Format(UriFormat, Server, MapMode, zoomLevel, x, y);
                return new Uri(Url);
            }
            return null;
        }

        private char MapModeConverter(GoogleTileTypes tiletype)
        {
            switch (tiletype)
            {
                case GoogleTileTypes.Hybrid:
                    {
                        return 'y';
                    }
                case GoogleTileTypes.Physical:
                    {
                        return 't';
                    }
                case GoogleTileTypes.Satellite:
                    {
                        return 's';
                    }
                case GoogleTileTypes.Street:
                    {
                        return 'm';
                    }
                case GoogleTileTypes.WaterOverlay:
                    {
                        return 'r';
                    }
            }
            return ' ';
        }
    }
}