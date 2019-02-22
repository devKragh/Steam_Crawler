using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SteamCrawler
{
	/// <summary>
	/// This class is for learning goals only
	/// 
	/// In an attemt to learn to get information from the internet 
	/// and process it in applications i made a trial though at tutorial 
	/// found here: https://medium.com/@thepen0411/web-crawling-tutorial-in-c-48d921ef956a
	/// 
	/// </summary>
	class CarRetrival
	{
		public static async Task<List<Car>> StartCrawlerAsync()
		{
			var url = "http://www.automobile.tn/neuf/bmw.3/";
			HttpClient client = new HttpClient();
			var html = await client.GetStringAsync(url);
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);

			var divs = htmlDocument.DocumentNode.Descendants("div").Where(
				node => node.GetAttributeValue("class", "").Equals("article_new_car article_last_modele")).ToList();
			Console.WriteLine(divs.Count.ToString());

			var cars = new List<Car>();

			foreach (var div in divs)
			{
				var priceDiv = div.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("price_last_modele")).ToList();

				var car = new Car
				{
					Model = div.Descendants("h2").FirstOrDefault().InnerText,
					Price = priceDiv[0].Descendants("span").FirstOrDefault().InnerText,
					Link = div.Descendants("a").FirstOrDefault().InnerText,
					ImageUrl = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value
				};
				cars.Add(car);
			}

			return cars;

		}

		public List<string> GetCarsAsString()
		{
			var listOfCars = StartCrawlerAsync().Result;
			var stringList = new List<string>();
			foreach (var car in listOfCars)
			{
				stringList.Add(car.ToString());
			}
			return stringList;
		}

	}
}
