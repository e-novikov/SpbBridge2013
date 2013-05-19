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
    public partial class LocationPoicy : PhoneApplicationPage
    {
        public LocationPoicy()
        {
            InitializeComponent();
            PlolicyText.Text = @"Приложение Мосты 2013 использует определение вашего местоположения. Если вы включили определение местоположения в настройках телефона и настройках приложения, ваши координаты будут использованы для отображения на карте. Приложение Мосты 2013 не отправляет и не хранят эти данные.
Вы можете выключить определение местоположения в настройках приложения, для этого зайдите в меню ""Настройки"" и передвиньте ползунок ""Геолокация"" в положение ""Выключено""";
        }
    }
}