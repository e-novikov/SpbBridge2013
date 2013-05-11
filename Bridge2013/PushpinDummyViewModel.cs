using System.Device.Location;
using System.Windows;

namespace Bridge2013
{
    public class PushpinBridgeViewModel
    {
        public Visibility Visibility { get; set; }
        public Visibility ContentVisibility { get; set; }
        public GeoCoordinate Position { get; set; }
        public int ZIndex { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public int Id { get; set; }
    }
}
