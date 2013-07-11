using System;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.DataAbstraction // ReSharper restore CheckNamespace
{
	[TestClass]
	public class PageVersionWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Delete_ShouldThrowANotImplementedException()
		{
			new PageVersionWrapper().Delete(new ContentReference());
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void LoadCommonDraft_ShouldThrowANotImplementedException()
		{
			new PageVersionWrapper().LoadCommonDraft(new ContentReference(), string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void SetCommonDraft_ShouldThrowANotImplementedException()
		{
			new PageVersionWrapper().SetCommonDraft(new ContentReference());
		}

		#endregion
	}
}