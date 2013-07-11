using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.Fakes;
using HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.ShimTests.DataAbstraction // ReSharper restore CheckNamespace
{
	[TestClass]
	public class PageVersionWrapperTest
	{
		#region Methods

		private static PageReference CreateRandomPageReference()
		{
			return new PageReference(DateTime.Now.Second, DateTime.Now.Millisecond, DateTime.Now.Second%5 == 0 ? "Test" : null);
		}

		private static PageVersion CreateRandomPageVersion()
		{
			return new PageVersion(CreateRandomPageReference(), Guid.NewGuid().ToString(), CreateRandomVersionStatus(), DateTime.Now, DateTime.Now.Second%3 == 0 ? null : "SavedBy", DateTime.Now.Second%5 == 0 ? null : "StatusChangedBy", DateTime.Now.Second, DateTime.Now.Second%2 == 0 ? "Test" : null, DateTime.Now.Second%2 == 0);
		}

		private static VersionStatus CreateRandomVersionStatus()
		{
			switch(DateTime.Now.Second%7)
			{
				case 0:
					return VersionStatus.CheckedIn;
				case 1:
					return VersionStatus.CheckedOut;
				case 2:
					return VersionStatus.DelayedPublish;
				case 3:
					return VersionStatus.NotCreated;
				case 4:
					return VersionStatus.PreviouslyPublished;
				case 5:
					return VersionStatus.Published;
				default:
					return VersionStatus.Rejected;
			}
		}

		[TestMethod]
		public void List_ShouldCallListOfTheWrappedPageVersionAndConvertTheResultToAnEnumerableOfContentVersion()
		{
			using(ShimsContext.Create())
			{
				bool listIsCalled = false;
				PageVersionCollection listValue = new PageVersionCollection();
				PageReference pageLinkValue = null;

				int items = DateTime.Now.Second%10;

				for(int i = 0; i < items; i++)
				{
					listValue.Add(CreateRandomPageVersion());
				}

				ShimPageVersion.ListPageReference = delegate(PageReference pageLink)
				{
					listIsCalled = true;
					pageLinkValue = pageLink;
					return listValue;
				};

				PageReference pageLinkParameter = CreateRandomPageReference();

				Assert.IsFalse(listIsCalled);
				IEnumerable<ContentVersion> contentVersions = new PageVersionWrapper().List(pageLinkParameter);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
				Assert.AreEqual(items, contentVersions.Count());
				Assert.IsTrue(listIsCalled);
			}
		}

		[TestMethod]
		public void MoreTestsNeeded()
		{
			Assert.Inconclusive("More tests needed for {0}.", typeof(PageVersionWrapper));
		}

		#endregion
	}
}