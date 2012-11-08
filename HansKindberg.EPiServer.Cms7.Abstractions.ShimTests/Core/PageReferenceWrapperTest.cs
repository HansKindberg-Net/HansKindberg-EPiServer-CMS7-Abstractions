//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//// ReSharper disable CheckNamespace
//namespace HansKindberg.EPiServer.Cms7.Abstractions.ShimTests.Core // ReSharper restore CheckNamespace
//{
//	[TestClass]
//	public class PageReferenceWrapperTest
//	{
//		[TestMethod]
//		public void IsExternalProvider_ShouldReturnIsRemoteOfTheWrappedPageReference()
//		{
//			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> { CallBase = true };
//			contentReferenceMock.Setup(contentReference => contentReference.ProviderName).Returns((string)null);
//			Assert.IsFalse(contentReferenceMock.Object.IsExternalProvider);
//		}
//		[TestMethod]
//		public void IsExternalProvider_IfTheProviderNameIsEmpty_ShouldReturnFalse()
//		{
//			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> { CallBase = true };
//			contentReferenceMock.Setup(contentReference => contentReference.ProviderName).Returns(string.Empty);
//			Assert.IsFalse(contentReferenceMock.Object.IsExternalProvider);
//		}
//		[TestMethod]
//		public void IsExternalProvider_IfTheProviderNameOnlyContainsWhiteSpaces_ShouldReturnTrue()
//		{
//			const string providerName = " ";
//			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> { CallBase = true };
//			contentReferenceMock.Setup(contentReference => contentReference.ProviderName).Returns(providerName);
//			Assert.IsFalse(string.IsNullOrEmpty(providerName));
//			Assert.IsTrue(contentReferenceMock.Object.IsExternalProvider);
//		}
//		[TestMethod]
//		public void IsExternalProvider_IfTheProviderNameIsNotNullAndIsNotEmpty_ShouldReturnTrue()
//		{
//			const string providerName = "Test";
//			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> { CallBase = true };
//			contentReferenceMock.Setup(contentReference => contentReference.ProviderName).Returns(providerName);
//			Assert.IsFalse(string.IsNullOrEmpty(providerName));
//			Assert.IsTrue(contentReferenceMock.Object.IsExternalProvider);
//		}
//	}
//}

