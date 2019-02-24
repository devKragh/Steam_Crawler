using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SteamCrawler.Logic.Tests
{
	[TestClass]
	public class UnitTest1
	{

		[TestMethod]
		public void TestMethod1()
		{
			SpecialsRetriver retriver = new SpecialsRetriver();
			decimal testDecimal = retriver.ConvertStringToDecimal("10,12€");

			Assert.AreEqual(0 ,decimal.Compare(testDecimal, 10.12m));
		}
	}
}
