using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace SteamCrawler
{
	class Program
	{
		static void Main(string[] args)
		{
			var listOfCars = CarRetrival.StartCrawlerAsync().Result;

			foreach (var car in listOfCars)
			{
				Console.WriteLine("\n" + car.ToString());
			}

			Console.ReadLine();

		}

		
	}
}
