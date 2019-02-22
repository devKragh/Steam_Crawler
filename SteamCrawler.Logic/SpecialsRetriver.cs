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
				CreateGameOffersFromNode(gameOffers, div);
			}
			return gameOffers;
		}

		private static void CreateGameOffersFromNode(List<GameOffer> gameOffers, HtmlNode div)
		{
			int tempIdHolder;
			var id = div.Ancestors().FirstOrDefault().ChildAttributes("data-ds-appid").FirstOrDefault();
			var strikeThoughPrice = div.Descendants("strike").FirstOrDefault();

			var priceAfterDiscount = div.SelectSingleNode(".//div[@class='col search_price discounted responsive_secondrow']");

			var discountPercentage = div.Descendants("div").Where(
							node => node.GetAttributeValue("class", "").Equals(
								"col search_discount responsive_secondrow")).FirstOrDefault();

			var userReviewScore = div.SelectSingleNode(".//div[@class='col search_reviewscore responsive_secondrow']/span").Attributes["data-tooltip-html"];

			var platformP = div.SelectSingleNode(".//div[@class='col search_name ellipsis']/p");

			if (id != null)
			{
				var newId = id.Value;

				if (int.TryParse(newId, out tempIdHolder))
				{
					var gameOffer = new GameOffer();
					gameOffer.SteamAppId = tempIdHolder;

					gameOffer.Title = div.Descendants("span").Where(
							node => node.GetAttributeValue("class", "Cant read title..").Equals(
							"title")).FirstOrDefault().InnerText;

					if (strikeThoughPrice != null)
					{
						gameOffer.PriceBeforeDiscount = strikeThoughPrice.InnerText.Trim();
					}
					else
					{
						gameOffer.PriceBeforeDiscount = "No price listed";
					}

					if (priceAfterDiscount != null)
					{
						gameOffer.PriceAfterDiscount = priceAfterDiscount.InnerHtml.Substring(priceAfterDiscount.InnerHtml.LastIndexOf('>')+1);
					}
					else
					{
						gameOffer.PriceAfterDiscount = "No price listed";
					}

					if (discountPercentage != null)
					{
						gameOffer.DiscountPercentage = discountPercentage.InnerText.Trim();
					}
					else
					{
						gameOffer.DiscountPercentage = "No price listed";
					}

					if (userReviewScore != null)
					{
						gameOffer.UserReviewScore = userReviewScore.Value.Trim();
					}

					gameOffer.StoreLink = div.Ancestors().FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value.Trim();

					foreach (var item in platformP.ChildNodes)
					{
						gameOffer.AddPlatform(item.GetAttributeValue("class","").Trim());
					}

					gameOffers.Add(gameOffer);
				}
			}
		}
	}
}
