using System;
using System.Collections.Generic;

namespace SteamCrawler.Model
{
	public class GameOffer
	{
		public long SteamAppId { get; set; }
		public string Title { get; set; }
		public string PriceBeforeDiscount { get; set; }
		public string PriceAfterDiscount { get; set; }
		public string DiscountPercentage { get; set; }
		public string UserReviewScore { get; set; }
		public List<string> Platforms { get; set; }
		public string StoreLink { get; set; }

		public GameOffer()
		{
			Platforms = new List<string>();
		}

		public void AddPlatform(string platform)
		{
			Platforms.Add(platform);
		}

	}
}
