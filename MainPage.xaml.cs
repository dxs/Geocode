using Geocode.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Geocode
{
	/// <summary>
	/// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
	/// </summary>
	public sealed partial class MainPage : Page
    {
		Matrix matrix;

		public MainPage()
        {
            this.InitializeComponent();
		}

		private async void SetupMap()
		{
			mainMap.MapServiceToken = Key.MapKey;
			Debug.WriteLine("Setup map");
			Ring.Visibility = Visibility.Visible;
			if (await Geo.RequestAccess() != true)
			{
				Debug.WriteLine("Failed to get access to position");
				return;
			}

			Coordinate c = await Geo.RequestCoordinate();
			matrix = new Matrix(c);
			await mainMap.TrySetViewAsync(new Geopoint(new BasicGeoposition() { Latitude = c.Latitude, Longitude = c.Longitude }), 19, 0, 0, MapAnimationKind.Bow);
			Ring.Visibility = Visibility.Collapsed;
			Debug.WriteLine("Done map");
		}

		private void MapControl_Loaded(object sender, RoutedEventArgs e)
		{
			SetupMap();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			if (matrix == null)
				return;
			matrix.ConstructURLFromCenter(matrix.GetDestinations());
			List<Coordinate> list = matrix.GetDestinations();
			foreach(Coordinate coord in list)
			{
				MapIcon icon = new MapIcon();
				Geopoint loc = new Geopoint(new BasicGeoposition() { Latitude = coord.Latitude, Longitude = coord.Longitude });
				icon.Location = loc;
				icon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 0.5);
				icon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Images/loc.png"));
				mainMap.MapElements.Add(icon);
			}
			Response res = new Response();
			res = await matrix.LaunchDispatcher();
		}
	}
}
