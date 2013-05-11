using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Bridge2013
{
    public class BridgesInfo
    {
        private static volatile BridgesInfo instance;
        private static object syncRoot = new Object();

        public List<Bridge> Bridges { get; set; }

        private BridgesInfo()
        {
            XDocument loadedData = XDocument.Load("BridgesList.xml");
            Bridges = new List<Bridge>(
                from query in loadedData.Descendants("Bridge")
                      select new Bridge
                      {
                          Description = (string)query.Value,
                          Latitude = (double)query.Attribute("latitude"),
                          Longitude = (double)query.Attribute("longitude"),
                          Id = (int)query.Attribute("id"),
                          TimeClose = (string)query.Attribute("timeClose"),
                          TimeOpen = (string)query.Attribute("timeOpen"),
                          Name = (string)query.Attribute("name"),
                      });
            
            foreach (Bridge b in Bridges)
            {
                string result = "";
                string [] closeTimes = b.TimeClose.Split(';');
                string [] openTimes = b.TimeOpen.Split(';');

                for (int i = 0; i < closeTimes.Length; i++)
                {
                    if (i != 0)
                        result += @"/";
                    result += openTimes[i]+"-"+closeTimes[i];
                }
                b.SheduleString = result;
            }
        }

        public static BridgesInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new BridgesInfo();
                    }
                }

                return instance;
            }
        }
    }
}
