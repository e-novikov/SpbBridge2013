using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Threading;

namespace Bridge2013
{
    public partial class BridgeInfo : PhoneApplicationPage
    {
        DateTime startDate;
        Color Blue;
        Color White;

        DispatcherTimer timer1 = new DispatcherTimer();
        Bridge bridge;

        public BridgeInfo()
        {
            startDate = new DateTime(1970, 1, 9, 0, 0, 00);
            InitializeComponent();
            Blue = Color.FromArgb(255, 0, 116, 255);
            White = Color.FromArgb(255, 255, 255, 255);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string id;
            timer1.Interval = new TimeSpan(0, 0, 0, 1);
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            if (NavigationContext.QueryString.TryGetValue("bridgeId", out id))
            {
                int bridgeId = int.Parse(id);
                bridge = BridgesInfo.Instance.Bridges.First<Bridge>(item => item.Id == bridgeId);
                PageName.Text = bridge.Name;
                Shedule.Text = bridge.SheduleString;
                UpdateTime();
            }
        }

        void UpdateTime()
        {
            DateTime myDate2 = DateTime.Now;
            TimeSpan nowTime = DateTime.Now.TimeOfDay;
            if (nowTime.TotalHours > 12)
            {
                nowTime -= new TimeSpan(24, 0, 0);
            }
            string[] closeTimes = bridge.TimeClose.Split(';');
            string[] openTimes = bridge.TimeOpen.Split(';');
            for (int i = 0; i < closeTimes.Length; i++)
            {
                string[] closeTimeSplitted = closeTimes[i].Split(':');
                string[] openTimeSplitted = openTimes[i].Split(':');


                int closeH = int.Parse(closeTimeSplitted[0]);
                int closeM = int.Parse(closeTimeSplitted[1]);

                int openH = int.Parse(openTimeSplitted[0]);
                int openM = int.Parse(openTimeSplitted[1]);

                TimeSpan openTime = new TimeSpan(openH, openM, 0);
                TimeSpan closeTime = new TimeSpan(closeH, closeM, 0);

                TimeSpan time1 = openTime - nowTime;
                TimeSpan time2 = closeTime - nowTime;

                if (time1.TotalSeconds > 0)
                {
                    Title.Text = "До разводки осталось";
                    Time.Text = string.Format("{0:00}:{1:00}:{2:00}", time1.Hours, time1.Minutes,time1.Seconds);
                    Colorize(White);
                    return;
                }
                else if (time1.TotalSeconds < 0 && time2.TotalSeconds > 0)
                {
                    Title.Text = "До сведения осталось";
                    Time.Text = string.Format("{0:00}:{1:00}:{2:00}", time2.Hours, time2.Minutes, time2.Seconds);
                    Colorize(Blue);
                    return;
                }
            }
            string[] oTimeSplitted = openTimes[0].Split(':');
            int oH = int.Parse(oTimeSplitted[0]);
            int oM = int.Parse(oTimeSplitted[1]);
            TimeSpan oTime = new TimeSpan(oH, oM, 0);
            Title.Text = "До разводки осталось";
            TimeSpan t = (oTime + new TimeSpan(24, 0, 0)) - nowTime;
            Time.Text = string.Format("{0:00}:{1:00}:{2:00}", t.Hours, t.Minutes, t.Seconds);
            Colorize(White);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void Colorize(Color c)
        {
            Time.Foreground = new SolidColorBrush(c);
            Title.Foreground = new SolidColorBrush(c);
        }

        private void Button1Tap(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MapPage.xaml?bridgeId=" + bridge.Id, UriKind.Relative));
        }
    }
}