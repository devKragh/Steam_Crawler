using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamCrawler.Logic
{
	public interface ISteamRetriver<T>
	{
		Task<List<T>> StartCrawlerAsync(string url);
	}
}
