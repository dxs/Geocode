using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media;

namespace Geocode.Logic
{
	class Drawing
	{
		public Geopath Path { get; set; }
		public SolidColorBrush Color { get; set; }

		public Drawing(List<Coordinate> list)
		{
			List<BasicGeoposition> positions = new List<BasicGeoposition>();
			foreach (Coordinate item in list)
				positions.Add(new BasicGeoposition() { Latitude = item.Latitude, Longitude = item.Longitude });
			Path = new Geopath(positions);
		}
	}
}
