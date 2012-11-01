using System;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentReferenceTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithContentIdParameter_ShouldSetTheId()
		{
			int randomContentId = GetRandomInteger();
			Assert.AreEqual(randomContentId, new ContentReference(randomContentId).ID);
		}

		private static int GetRandomInteger()
		{
			int randomInteger = DateTime.Now.Millisecond;
			return DateTime.Now.Second%2 == 0 ? randomInteger : -randomInteger;
		}

		#endregion
	}
}