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

		public string ConstructURLFromCenter()
		{
			string tmp = API + origin + Center.Latitude + "," + Center.Longitude;
			tmp += destination;
			List<Coordinate> l = GetDestinations();
			foreach(Coordinate item in l)
			{
				tmp += item.Latitude + "," + item.Longitude;
				if (item != l.Last())
					tmp += "|";
			}
			tmp += key + Key.DistanceMatrixkey;
			return tmp;
		}

		public async void ParseJson()
		{
			var uri = new Uri(ConstructURLFromCenter());
			var httpClient = new HttpClient();
			var content = await httpClient.GetStringAsync(uri);
			JObject json = await Task.Run(() => JObject.Parse(content));

			if (json["status"].ToString() != "OK")
			{
				Debug.WriteLine("Error first parsing");
				return;
			}

			Response reponse = new Response();
			reponse.Status = json["status"].ToString();
			reponse.Origin = json["origin_addresses"][0].ToString();
			foreach (var item in json["destination_addresses"])
				reponse.Destination.Add(item.ToString());
			foreach(var item in json["rows"])
			{
				reponse.Rows = item["elements"][0]
			}
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
