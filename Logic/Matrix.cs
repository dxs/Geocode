using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Geocode.Logic
{
	class Matrix
	{
		private Coordinate Center;
		private const string API = @"https://maps.googleapis.com/maps/api/distancematrix/json?";
		private const string origin = @"origins=";
		private const string destination = @"&destinations=";
		private const string mode = @"&mode=";
		private const string key = @"&key=";

		public Matrix(Coordinate center)
		{
			Center = center;
		}  

		public string ConstructURLFromCenter(List<Coordinate> list)
		{
			string tmp = API + origin + Center.Latitude + "," + Center.Longitude;
			tmp += destination;
			foreach(Coordinate item in list)
			{
				tmp += item.Latitude + "," + item.Longitude;
				if (item != list.Last())
					tmp += "|";
			}
			tmp += key + Key.DistanceMatrixkey;
			return tmp;
		}

		public List<Coordinate> GetDestinations()
		{
			List<Coordinate> listDestinations = new List<Coordinate>();

			/*in km*/
			double radius = 0;
			int radiusIteration = 30;
			int angleIteration = 0;
			double angle = 0;
			for (int i = 0; i < radiusIteration; i++)
			{
				radius += 1;
				angle = 0;
				angleIteration += 5;
				double basicAngle = 2 * Math.PI / angleIteration;
				for (int j = 0; j < angleIteration; j++)
				{
					Coordinate t = new Coordinate();
					t.Latitude = AddKmToLatitude(Center.Latitude, radius * Math.Sin(angle+j*angleIteration));
					t.Longitude = AddKmToLongitude(Center.Latitude, Center.Longitude, radius * Math.Cos(angle+j*angleIteration));
					listDestinations.Add(t);
				}
			}
			return listDestinations;
		}

		public async Task<Response> LaunchDispatcher()
		{
			Response response = new Response();

			List<Coordinate> list = new List<Coordinate>();
			list.AddRange(GetDestinations());
			for (int i = 0; i < list.Count / 100; i++)
			{
				var uri = new Uri(ConstructURLFromCenter(list.GetRange(list.Count/100*i,100)));
				HttpClient client = new HttpClient();
				var content = await client.GetStringAsync(uri);
				JObject jReponse = await Task.Run(() => JObject.Parse(content));
				Response r = (Response)jReponse.ToObject(typeof(Response));
				if (r.Status != "OK") continue;
				foreach(var item in r.Destination_addresses)
					response.Destination_addresses.Add(item);
				foreach (var item in r.Rows)
					response.Rows.Add(item);
			}
			return response;
		}

		private double AddKmToLatitude(double latitude, double km)
		{
			double tmp = km / 110.574;
			return latitude + tmp;
		}

		private double AddKmToLongitude(double latitude, double longitude, double km)
		{
			double radian = latitude / 180 * Math.PI;
			double tmp = km / 111.320 * Math.Cos(radian);
			return longitude + tmp;
		}
	}
}
