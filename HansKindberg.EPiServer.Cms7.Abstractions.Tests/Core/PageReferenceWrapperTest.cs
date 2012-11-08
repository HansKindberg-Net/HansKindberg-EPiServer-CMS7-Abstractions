using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.Core // ReSharper restore CheckNamespace
{
	[TestClass]
	public class PageReferenceWrapperTest
	{
		#region Fields

		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareTo_IfTheObjectParameterIsNotOfTypePageReferenceWrapper_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			CreateRandomPageReferenceWrapper().CompareTo(new object());
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CompareTo_IfTheObjectParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ReturnValueOfPureMethodIsNotUsed
				CreateRandomPageReferenceWrapper().CompareTo(null);
				// ReSharper restore ReturnValueOfPureMethodIsNotUsed	
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "obj")
					throw;
			}
		}

		[TestMethod]
		public void CompareTo_IfTheObjectParameterIsTheSameAsTheInstance_ShouldReturnZero()
		{
			PageReferenceWrapper pageReferenceWrapper = CreateRandomPageReferenceWrapper();
			Assert.AreEqual(0, pageReferenceWrapper.CompareTo(pageReferenceWrapper));
		}

		[TestMethod]
		public void Constructor_IfThePageReferenceParameterIsNotNull_ShouldSetThePageReferenceProperty()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference, new PageReferenceWrapper(pageReference).PageReference);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.EPiServer.Cms7.Abstractions.Core.PageReferenceWrapper")]
		public void Constructor_IfThePageReferenceParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new PageReferenceWrapper(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "pageReference")
					throw;
			}
		}

		private static bool CreateRandomBoolean()
		{
			return _random.Next(0, int.MaxValue)%2 == 0;
		}

		private static int CreateRandomInteger()
		{
			return _random.Next(int.MinValue, int.MaxValue);
		}

		private static PageReference CreateRandomPageReference()
		{
			PageReference pageReference = new PageReference(CreateRandomInteger(), CreateRandomInteger(), CreateRandomProviderName(), CreateRandomBoolean());

			if(CreateRandomBoolean())
				pageReference.MakeReadOnly();

			return pageReference;
		}

		private static PageReferenceWrapper CreateRandomPageReferenceWrapper()
		{
			return new PageReferenceWrapper(CreateRandomPageReference());
		}

		private static string CreateRandomProviderName()
		{
			switch(_random.Next(0, int.MaxValue)%4)
			{
				case 0:
					return null;
				case 1:
					return string.Empty;
				case 2:
					return " ";
				default:
					return Guid.NewGuid().ToString();
			}
		}

		[TestMethod]
		public void Equals_ShouldCallEqualsOfTheWrappedPageReference()
		{
			Mock<PageReference> pageReferenceMock = new Mock<PageReference> {CallBase = true};
			pageReferenceMock.Setup(pageReference => pageReference.Equals(It.IsAny<object>())).Returns(false);
			pageReferenceMock.Verify(pageReference => pageReference.Equals(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(new PageReferenceWrapper(pageReferenceMock.Object).Equals(CreateRandomPageReferenceWrapper()));
			pageReferenceMock.Verify(pageReference => pageReference.Equals(It.IsAny<object>()), Times.AtLeastOnce());
		}

		[TestMethod]
		public void GetHashCode_ShouldCallGetHashCodeOfTheWrappedPageReference()
		{
			Mock<PageReference> pageReferenceMock = new Mock<PageReference> {CallBase = true};
			pageReferenceMock.Setup(pageReference => pageReference.GetHashCode()).Returns(0);
			pageReferenceMock.Verify(pageReference => pageReference.GetHashCode(), Times.Never());
			Assert.IsNotNull(new PageReferenceWrapper(pageReferenceMock.Object).GetHashCode());
			pageReferenceMock.Verify(pageReference => pageReference.GetHashCode(), Times.Once());
		}

		[TestMethod]
		public void ToString_ShouldCallToStringOfTheWrappedPageReference()
		{
			Mock<PageReference> pageReferenceMock = new Mock<PageReference> {CallBase = true};
			pageReferenceMock.Setup(pageReference => pageReference.ToString()).Returns(string.Empty);
			pageReferenceMock.Verify(pageReference => pageReference.ToString(), Times.Never());
			Assert.IsNotNull(new PageReferenceWrapper(pageReferenceMock.Object).ToString());
			pageReferenceMock.Verify(pageReference => pageReference.ToString(), Times.Once());
		}

		#endregion
	}
}