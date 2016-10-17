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
		public string[] Origin_addresses { get; set; }
		public string[] Destination_addresses { get; set; }
		public Row[] Rows { get; set; }

		public Response()
		{

		}
	}

	class Row
	{
		public Element[] Elements {get;set;}
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
