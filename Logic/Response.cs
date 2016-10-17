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
		public List<string> Origin_addresses { get; set; }
		public List<string> Destination_addresses { get; set; }
		public List<Row> Rows { get; set; }

		public Response()
		{
			Origin_addresses = new List<string>();
			Destination_addresses = new List<string>();
			Rows = new List<Row>();
		}
	}

	class Row
	{
		public List<Element> Elements {get;set;}

		public Row()
		{
			Elements = new List<Element>();
		}
	}

	class Element
	{
		public string Status { get; set; }
		public Dur Duration { get; set; }
		public Dist Distance { get; set; }
		public Element()
		{

		}
	}

	class Dist
	{
		public int Value { get; set; }
		public string Text { get; set; }
		public Dist()
		{

		}
	}

	class Dur
	{
		public int Value { get; set; }
		public string Text { get; set; }
		public Dur()
		{

		}
	}
}
