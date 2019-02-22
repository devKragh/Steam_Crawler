using System;
using System.Collections.Generic;

namespace SteamCrawler.Model
{
	public class GameOffer
	{
		public string Title { get; set; }
		public string Price { get; set; }
		public string Discount { get; set; }
		public string UserReviewScore { get; set; }
		public List<string> Platforms { get; set; }
		public string Link { get; set; }

		public void AddPlatform(string platform)
		{
			Platforms.Add(platform);
		}

	}
}
