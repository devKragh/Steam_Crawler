using SteamCrawler.Logic;
using SteamCrawler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
	/// <summary>
	/// Just for running test before using GUI
	/// </summary>
	class Program
	{
		static bool testing = true;
		static void Main(string[] args)
		{
			SpecialsRetriver retriver = new SpecialsRetriver();
			int currentNumberOfOffers = 0;
			int indexer = 2;
			//Refactor what goes on below
			try
			{
				if (!testing)
				{
					do
					{
						var retrivedOffers = retriver.StartCrawlerAsync(
										"https://store.steampowered.com/search/?specials=1&page=" + indexer).Result;
						foreach (GameOffer item in retrivedOffers)
						{
							Console.WriteLine(item.Title);
						}
						currentNumberOfOffers = retrivedOffers.Count;
						indexer++;
						Console.WriteLine("CurrentPage: " + indexer);
					} while (currentNumberOfOffers > 0);
				}
				else
				{
					var retrivedOffers = retriver.StartCrawlerAsync(
										"https://store.steampowered.com/search/?specials=1&page=2").Result;

					foreach (GameOffer item in retrivedOffers)
					{
						Console.WriteLine(item.SteamAppId + "\n" +
							item.Title + "\n" +
							item.PriceBeforeDiscount + "\n" +
							item.PriceAfterDiscount + "\n" +
							item.DiscountPercentage + "\n" +
							item.UserReviewScore + "\n" +
							item.StoreLink);

						foreach (var platform in item.Platforms)
						{
							Console.WriteLine(platform);
						}

						Console.WriteLine("\n\n");
					}
				}
			}
			catch (HttpRequestException e)
			{
				
				Console.WriteLine(e.StackTrace);
				Console.WriteLine("Http problem");
			}
			Console.ReadLine();

		}
	}
}
