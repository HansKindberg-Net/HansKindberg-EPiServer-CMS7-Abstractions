using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentReferenceTest
	{
		#region Methods

		[TestMethod]
		public void APageReferenceShouldBeAbleToBePassedAsParameterToAMethodWithAContentReferenceParameter()
		{
			ContentReference contentReference = MethodWithContentReferenceParameter(new PageReference(1));
			Assert.AreEqual(1, contentReference.ID);
		}

		[TestMethod]
		public void Constructor_WithContentIdParameter_ShouldSetTheId()
		{
			int randomContentId = GetRandomInteger();
			Assert.AreEqual(randomContentId, new ContentReference(randomContentId).ID);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "EPiServer.Core.ContentReference")]
		public void Constructor_WithPageReferenceParameter_IfThePageReferenceParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new ContentReference((PageReference) null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "pageReference")
					throw;
			}
		}

		[TestMethod]
		public void EmptyReference_ShouldBeReadOnly()
		{
			Assert.IsTrue(ContentReference.EmptyReference.IsReadOnly);
		}

		[TestMethod]
		public void EqualityOperator_IfBothParametersAreNull_ShouldReturnTrue()
		{
			// ReSharper disablex EqualExpressionComparison
			Assert.IsTrue((ContentReference) null == null);
			// ReSharper restore EqualExpressionComparison
		}

		[TestMethod]
		public void EqualityOperator_IfBothParametersAreTheSameInstance_ShouldReturnTrue()
		{
			ContentReference contentReference = new ContentReference();
#pragma warning disable 1718
			// ReSharper disable EqualExpressionComparison
			Assert.IsTrue(contentReference == contentReference);
			// ReSharper restore EqualExpressionComparison
#pragma warning restore 1718
		}

		[TestMethod]
		public void EqualityOperator_IfOneParametersIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(null == new ContentReference());
			Assert.IsFalse(new ContentReference() == null);
		}

		[TestMethod]
		public void EqualityOperator_IfTheParametersAreNotNullAndAreDifferentInstances_ShouldCallEqualsOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object == Mock.Of<ContentReference>()); // Will not throw a NotImplementedException because the method is not setup and CallBase is not set to true on the mock.
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Once());
		}

		private static int GetRandomInteger()
		{
			int randomInteger = DateTime.Now.Millisecond;
			return DateTime.Now.Second%2 == 0 ? randomInteger : -randomInteger;
		}

		[TestMethod]
		public void ImplicitOperator_Test()
		{
			Assert.Inconclusive("Implement one or more tests. Not shore wat to name it. Look at PageReferenceExtension");
		}

		[TestMethod]
		public void InequalityOperator_IfBothParametersAreNull_ShouldReturnFalse()
		{
			Assert.IsFalse((ContentReference) null != null);
		}

		[TestMethod]
		public void InequalityOperator_IfBothParametersAreTheSameInstance_ShouldReturnFalse()
		{
			ContentReference contentReference = new ContentReference();
#pragma warning disable 1718
			// ReSharper disable EqualExpressionComparison
			Assert.IsFalse(contentReference != contentReference);
			// ReSharper restore EqualExpressionComparison
#pragma warning restore 1718
		}

		[TestMethod]
		public void InequalityOperator_IfOneParametersIsNull_ShouldReturnTrue()
		{
			Assert.IsTrue(null != new ContentReference());
			Assert.IsTrue(new ContentReference() != null);
		}

		[TestMethod]
		public void InequalityOperator_IfTheParametersAreNotNullAndAreDifferentInstances_ShouldCallEqualsOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object != Mock.Of<ContentReference>());
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Once());
		}

		private static ContentReference MethodWithContentReferenceParameter(ContentReference contentReference)
		{
			return contentReference;
		}

		[TestMethod]
		public void SelfReference_ShouldBeReadOnly()
		{
			Assert.IsTrue(ContentReference.SelfReference.IsReadOnly);
		}

		[TestMethod]
		public void ToString_ShouldCallToStringOnTheWrappedPageReference()
		{
			Mock<PageReference> pageReferenceMock = new Mock<PageReference>();
			pageReferenceMock.Verify(contentReference => contentReference.ToString(), Times.Never());
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new ContentReference(pageReferenceMock.Object).ToString();
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
			pageReferenceMock.Verify(contentReference => contentReference.ToString(), Times.Once());
		}

		#endregion
	}
}