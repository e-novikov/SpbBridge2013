using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge2013
{
    public enum Bridges
    {
        AleksandraNevskogo=1,
        Birzhevoy=2,
        Blagoveshensky=9,
        Bolsheohtinsky=3,
        Volodarsky=4,
        Dvortsovy=10,
        Liteiny=5,
        Troicky=6,
        Tychkov=7,
        Finlandsky=8
    }

    public class Bridge
    {
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Id {get;set;}
        public string TimeOpen { get; set; }
        public string TimeClose { get; set; }
        public string Name { get; set; }
        public string SheduleString { get; set; }
    }
}
