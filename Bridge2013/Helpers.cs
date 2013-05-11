using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace Bridge2013
{
    public static class Helpers
    {
        public static IObservable<GeoPositionStatusChangedEventArgs> GetStatusChangedEventStream(this GeoCoordinateWatcher watcher)
        {
            return Observable.Create<GeoPositionStatusChangedEventArgs>(observer =>
            {
                EventHandler<GeoPositionStatusChangedEventArgs> handler = (s, e) =>
                {
                    observer.OnNext(e);
                };
                watcher.StatusChanged += handler;
                return () => { watcher.StatusChanged -= handler; };
            }
            );
        }

        public static IObservable<GeoPositionChangedEventArgs<GeoCoordinate>> GetPositionChangedEventStream(this GeoCoordinateWatcher watcher)
        {
            return Observable.Create<GeoPositionChangedEventArgs<GeoCoordinate>>(observable =>
            {
                EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> handler = (s, e) =>
                {
                    observable.OnNext(e);
                };
                watcher.PositionChanged += handler;
                return () => { watcher.PositionChanged -= handler; };
            }
            );
        }
    }
}
