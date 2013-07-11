using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.Tests.DataAbstraction
{
	[TestClass]
	public class ContentVersionTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_ShouldAcceptNullValues()
		{
			ContentVersion contentVersion = new ContentVersion(null, null, VersionStatus.NotCreated, DateTime.MinValue, null, null, int.MinValue, null, false, false);
			Assert.IsNotNull(contentVersion);
		}

		[TestMethod]
		public void MoreTestsNeeded()
		{
			Assert.Inconclusive("More tests needed for {0}.", typeof(ContentVersion));
		}

		#endregion
	}
}