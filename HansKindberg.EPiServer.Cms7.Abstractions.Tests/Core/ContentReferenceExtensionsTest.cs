using System;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentReferenceExtensionsTest
	{
		#region Methods

		[TestMethod]
		public void ToPageReference_IfTheContentLinkParameterValueIsNotNull_ShouldReturnTheWrappedPageReference()
		{
			ContentReference contentLink = new ContentReference(DateTime.Now.Millisecond, DateTime.Now.Second);
			Assert.AreEqual(contentLink.PageReference, contentLink.ToPageReference());
		}

		[TestMethod]
		public void ToPageReference_IfTheContentLinkParameterValueIsNull_ShouldReturnNull()
		{
			Assert.IsNull(((ContentReference) null).ToPageReference());
		}

		#endregion
	}
}