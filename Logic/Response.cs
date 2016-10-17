using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geocode.Logic
{
	class Response
	{
		public string Status { get; set; }
		public string Origin { get; set; }
		public List<string> Destination { get; set; }
		public List<Elements> Rows { get; set; }

		public Response()
		{

		}
	}

	class Elements
	{
		public string Status { get; set; }
		public Duration Dur { get; set; }
		public Distance Dist { get; set; }
		public Elements()
		{
		}
	}

	class Distance
	{
		public string Status { get; set; }
		public int Meter { get; set; }
		public string Text { get; set; }
		public Distance()
		{

		}
	}

	class Duration
	{
		public string Status { get; set; }
		public int Second { get; set; }
		public string Text { get; set; }
	}
}
