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
			var strikeThoughPrice = div.Descendants("strike").FirstOrDefault(); //Decimal

			var priceAfterDiscount = div.SelectSingleNode(".//div[@class='col search_price discounted responsive_secondrow']"); //Decimal

			var discountPercentage = div.Descendants("div").Where(
							node => node.GetAttributeValue("class", "").Equals(
								"col search_discount responsive_secondrow")).FirstOrDefault(); //Int

			var userReviewScore = div.SelectSingleNode(".//div[@class='col search_reviewscore responsive_secondrow']/span").Attributes["data-tooltip-html"]; //Percentage int

			var platformP = div.SelectSingleNode(".//div[@class='col search_name ellipsis']/p"); //int 1,2 or 3 prob enum

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
						gameOffer.PriceBeforeDiscount = ConvertStringToDecimal(strikeThoughPrice.InnerText.Trim());
					}
					else
					{
						gameOffer.PriceBeforeDiscount = 0m;
					}

					if (priceAfterDiscount != null)
					{
						var temp = priceAfterDiscount.InnerHtml.Substring(priceAfterDiscount.InnerHtml.LastIndexOf('>') + 1).Trim();
						gameOffer.PriceAfterDiscount = ConvertStringToDecimal(temp);
					}
					else
					{
						gameOffer.PriceAfterDiscount = 0m;
					}

					if (discountPercentage != null)
					{
						gameOffer.DiscountPercentage = ConvertPercentageStringToInt(discountPercentage.InnerText.Trim());
					}
					else
					{
						gameOffer.DiscountPercentage = 0;
					}

					if (userReviewScore != null)
					{
						gameOffer.UserReviewScore = ConvertPercentageStringToInt(userReviewScore.Value.Trim());
					}

					gameOffer.StoreLink = div.Ancestors().FirstOrDefault().ChildAttributes("href").FirstOrDefault().Value.Trim();

					foreach (var item in platformP.ChildNodes)
					{
						gameOffer.AddPlatform(ConvertStringToPlatformEnum(item.GetAttributeValue("class", "").Trim()));
					}

					gameOffers.Add(gameOffer);
				}
			}
		}

		private static decimal ConvertStringToDecimal(string stringToConvert)
		{
			stringToConvert = stringToConvert.Remove(stringToConvert.Length - 1); //Remove symbol
			stringToConvert = stringToConvert.Replace(',', '.');
			decimal res;
			Console.WriteLine(stringToConvert);
			if (decimal.TryParse(stringToConvert, out res))
			{
				return res;

			} else{
				return 0;
			}
		}

		private static int ConvertPercentageStringToInt(string stringToConvert)
		{
			if (stringToConvert.Length <= 0)
			{
				return -1;
			}
			int res;
			stringToConvert = stringToConvert.Remove(stringToConvert.IndexOf('%'));
			stringToConvert = stringToConvert.Remove(0, stringToConvert.Length - 2);
			Console.WriteLine(stringToConvert);
			if (int.TryParse(stringToConvert, out res))
			{
				return res;
			}
			else
			{
				return 0;
			}
		}

		private static Platform ConvertStringToPlatformEnum(string stringToConvert)
		{
			Console.WriteLine(stringToConvert);

			Platform res;
			if (stringToConvert.Contains("win"))
			{
				res = Platform.Win;
			}
			else if(stringToConvert.Contains("mac"))
			{
				res = Platform.Mac;
			}
			else if(stringToConvert.Contains("linux"))
			{
				res = Platform.Linux;
			}
			else
			{
				res = Platform.None;
			}

			return res;
		}

		
	}
}
