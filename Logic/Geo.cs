using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Geocode.Logic
{
	public static class Geo
	{
		private static Geolocator GPS;

		public async static Task<bool?> RequestAccess()
		{
			var accessStatus = await Geolocator.RequestAccessAsync();

			switch (accessStatus)
			{
				case GeolocationAccessStatus.Allowed:
					return true;
					// Subscribe to the PositionChanged event to get location updates.
				case GeolocationAccessStatus.Denied:
					return false;

				case GeolocationAccessStatus.Unspecified:
					return null;
			}
			return null;
		}

		public async static Task<Coordinate> RequestCoordinate()
		{
			GPS = new Geolocator { ReportInterval = 500 };
			GPS.DesiredAccuracy = PositionAccuracy.Default;
			Geoposition pos = await GPS.GetGeopositionAsync();
			return new Coordinate(pos.Coordinate.Point.Position.Latitude, pos.Coordinate.Point.Position.Longitude);
		}
	}
}
