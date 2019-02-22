using HtmlAgilityPack;
using SteamCrawler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamCrawler.Logic
{
	public class SpecialsRetriver : ISteamRetriver<GameOffer>
	{
		HttpClient client;

		public SpecialsRetriver()
		{
			client = new HttpClient();
		}

		public async Task<List<GameOffer>> StartCrawlerAsync(string url)
		{
			var html = await client.GetStringAsync(url);
			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(html);

			var divs = htmlDoc.DocumentNode.Descendants("div").Where(
				node => node.GetAttributeValue("class", "").Equals(
				"responsive_search_name_combined")).ToList();

			var gameOffers = new List<GameOffer>();

			foreach (var div in divs)
			{
				var gameOffer = new GameOffer
				{
					Title = div.Descendants("span").Where(
						node => node.GetAttributeValue("class", "").Equals(
						"title")).FirstOrDefault().InnerText,
					Price = div.Descendants("div").Where(node => node.GetAttributeValue("class","").Equals("col search_price discounted responsive_secondrow")).FirstOrDefault().InnerText,
					Discount = div.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("col search_discount responsive_secondrow")).FirstOrDefault().InnerText,
					Link = div.Ancestors().FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value 

				};
				gameOffers.Add(gameOffer);
			}
			return gameOffers;
		}
	}
}
