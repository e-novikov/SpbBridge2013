using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Bridge2013
{
    public static class Settings
    {
        public static bool IsNavigationEnabled
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("IsNavigationEnabled"))
                    return false;
                return (bool)IsolatedStorageSettings.ApplicationSettings["IsNavigationEnabled"];
            }
            set
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("IsNavigationEnabled"))
                    IsolatedStorageSettings.ApplicationSettings.Add("IsNavigationEnabled", value);
                else
                    IsolatedStorageSettings.ApplicationSettings["IsNavigationEnabled"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}
