using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCrawler
{
	class Car
	{
		public string Model { get; set; }
		public string Price { get; set; }
		public string Link { get; set; }
		public string ImageUrl { get; set; }

		public override string ToString()
		{

			return "Model: " + Model +
					"\nPrice: " + Price +
					"\nLink: " + Link +
					"\nImage Link: " + ImageUrl;
		}

	}
}
