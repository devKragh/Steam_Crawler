using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCrawler.Logic.Exception
{
	public class AlreadyAddedToCollectionException : SystemException
	{
		string errorMessage;

		public AlreadyAddedToCollectionException()
		{
			errorMessage = "Creator did not specify reason where the error occured";
		}

		public AlreadyAddedToCollectionException(string errorDescription)
		{
			errorMessage = errorDescription;
		}

	}
}
