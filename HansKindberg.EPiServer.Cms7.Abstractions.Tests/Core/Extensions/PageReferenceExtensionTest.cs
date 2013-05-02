using System;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.Core.Extensions // ReSharper restore CheckNamespace
{
	[TestClass]
	public class PageReferenceExtensionTest
	{
		#region Methods

		[TestMethod]
		public void ToContentReference_IfThePageLinkParameterValueIsNotNull_ShouldReturnAContentReferenceWithThePageLinkWrapped()
		{
			PageReference pageLink = new PageReference(DateTime.Now.Millisecond, DateTime.Now.Second);
			Assert.AreEqual(pageLink, pageLink.ToContentReference().PageReference);
		}

		[TestMethod]
		public void ToContentReference_IfThePageLinkParameterValueIsNull_ShouldReturnNull()
		{
			Assert.IsNull(((PageReference) null).ToContentReference());
		}

		#endregion
	}
}