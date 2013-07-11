using System;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.DataAbstraction.Extensions // ReSharper restore CheckNamespace
{
	[TestClass]
	public class ContentVersionExtensionTest
	{
		#region Methods

		private static ContentReference CreateRandomContentReference()
		{
			return new ContentReference(DateTime.Now.Second, DateTime.Now.Millisecond, DateTime.Now.Second%5 == 0 ? "Test" : null);
		}

		private static ContentVersion CreateRandomContentVersion()
		{
			return new ContentVersion(CreateRandomContentReference(), Guid.NewGuid().ToString(), CreateRandomVersionStatus(), DateTime.Now, DateTime.Now.Second%3 == 0 ? null : "SavedBy", DateTime.Now.Second%5 == 0 ? null : "StatusChangedBy", DateTime.Now.Second, DateTime.Now.Second%2 == 0 ? "Test" : null, DateTime.Now.Second%2 == 0, DateTime.Now.Second%2 != 0);
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
		public void ToPageVersion_IfTheContentVersionParameterValueIsNotNull_ShouldReturnAPageVersionWithEqualProperties()
		{
			ContentVersion contentVersion = CreateRandomContentVersion();
			PageVersion pageVersion = contentVersion.ToPageVersion();
			Assert.IsNotNull(pageVersion);
			// ReSharper disable SuspiciousTypeConversion.Global
			Assert.IsTrue(contentVersion.Equals(pageVersion));
			Assert.IsTrue(contentVersion.ContentLink.Equals(pageVersion.ID));
			// ReSharper restore SuspiciousTypeConversion.Global
			Assert.AreEqual(contentVersion.IsMasterLanguageBranch, pageVersion.IsMasterLanguageBranch);
			Assert.AreEqual(contentVersion.LanguageBranch, pageVersion.LanguageBranch);
			Assert.AreEqual(contentVersion.MasterVersionID, pageVersion.MasterVersionID);
			Assert.AreEqual(contentVersion.Name, pageVersion.Name);
			Assert.AreEqual(contentVersion.Saved, pageVersion.Saved);
			Assert.AreEqual(contentVersion.SavedBy, pageVersion.SavedBy);
			Assert.AreEqual(contentVersion.Status, pageVersion.Status);
			Assert.AreEqual(contentVersion.StatusChangedBy, pageVersion.StatusChangedBy);
		}

		[TestMethod]
		public void ToPageVersion_IfTheContentVersionParameterValueIsNull_ShouldReturnNull()
		{
			Assert.IsNull(((ContentVersion) null).ToPageVersion());
		}

		#endregion
	}
}