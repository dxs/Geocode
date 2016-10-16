using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Geocode.Logic
{
	static class Coloring
	{
		private static SolidColorBrush green = new SolidColorBrush(Colors.Green);
		private static SolidColorBrush blue = new SolidColorBrush(Colors.Blue);
		private static SolidColorBrush red = new SolidColorBrush(Colors.Red);
		private static SolidColorBrush orange = new SolidColorBrush(Colors.Orange);
		private static SolidColorBrush yellow = new SolidColorBrush(Colors.Yellow);

		public static SolidColorBrush Green { get { return green; } }
		public static SolidColorBrush Blue { get { return blue; } }
		public static SolidColorBrush Red { get { return red; } }
		public static SolidColorBrush Orange { get { return orange; } }
		public static SolidColorBrush Yellow { get { return yellow; } }

		private static double opacity = 1.0;
		public static double Opacity { get { return opacity; } set { opacity = value; } }

		/// <summary>
		/// To get a list containing a color with all of it's gradients
		/// </summary>
		/// <param name="original">The original color to grad</param>
		/// <param name="nDegrades">number of gradients wanted, default is 10</param>
		/// <returns></returns>
		public static List<SolidColorBrush> GetDegradeColor(SolidColorBrush original, int nDegrades = 10)
		{
			List<SolidColorBrush> list = new List<SolidColorBrush>();
			double percent = 1;
			for(int i = 0; i < nDegrades; i++)
			{
				original.Opacity = percent - 1 / nDegrades;
				list.Add(original);
			}
			return list;
		}
	}
}
