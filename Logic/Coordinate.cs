using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocode.Logic
{
	public class Coordinate
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public Coordinate()
		{

		}

		public Coordinate(double _latitude, double _longitude)
		{
			Latitude = _latitude;
			Longitude = _longitude;
		}
	}
}
