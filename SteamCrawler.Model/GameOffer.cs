using System;
using System.Collections.Generic;

namespace SteamCrawler.Model
{
	public enum Platform
	{
		Win, Mac, Linux, None
	}
	public class GameOffer
	{
		public int SteamAppId { get; set; }
		public string Title { get; set; }
		public decimal PriceBeforeDiscount { get; set; }
		public decimal PriceAfterDiscount { get; set; }
		public int DiscountPercentage { get; set; }
		public int UserReviewScore { get; set; }
		public HashSet<Platform> Platforms { get; set; }
		public string StoreLink { get; set; }

		public GameOffer()
		{
			Platforms = new HashSet<Platform>();
		}

		public void AddPlatform(Platform platform)
		{
			if (Platforms.Contains(platform))
			{
				throw new Exception("Gameoffer already has that platform");
			}
			else
			{
				if (platform != Platform.None)
				{
					Platforms.Add(platform);
				}

			}
		}


		public override string ToString()
		{
			string res = "Steam App Id: " + SteamAppId + "\n" +
				"Title: " + Title + "\n" +
				"Normal Price: " + PriceBeforeDiscount + "\n" +
				"Discounted Price: " + PriceAfterDiscount + "\n" +
				"Discound Percentage: " + DiscountPercentage + "\n" +
				"Review score: " + UserReviewScore + "/100\n" +
				"Store Link: " + StoreLink + "\n";

			foreach (Platform platform in Platforms)
			{
				res += platform.ToString() + ", ";
			}

			res += "\n\n";

			return res;
		}
	}
}
