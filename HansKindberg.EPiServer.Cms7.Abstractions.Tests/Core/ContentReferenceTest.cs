using System;
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
		public void EqualityOperator_IfBothParametersAreNull_ShouldReturnTrue()
		{
			// ReSharper disable EqualExpressionComparison
			Assert.IsTrue((ContentReference) null == null);
			// ReSharper restore EqualExpressionComparison
		}

		[TestMethod]
		public void EqualityOperator_IfBothParametersAreTheSameInstance_ShouldReturnTrue()
		{
			ContentReference contentReference = Mock.Of<ContentReference>();
#pragma warning disable 1718
			// ReSharper disable EqualExpressionComparison
			Assert.IsTrue(contentReference == contentReference);
			// ReSharper restore EqualExpressionComparison
#pragma warning restore 1718
		}

		[TestMethod]
		public void EqualityOperator_IfOneParametersIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(null == Mock.Of<ContentReference>());
			Assert.IsFalse(Mock.Of<ContentReference>() == null);
		}

		[TestMethod]
		public void EqualityOperator_IfTheParametersAreNotNullAndAreDifferentInstances_ShouldCallEqualsOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object == Mock.Of<ContentReference>()); // Will not throw a NotImplementedException because the method is not setup and CallBase is not set to true on the mock.
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void Equals_ShouldThrowANotImplementedException_BecauseTheMethodMustBeImplementedInTheDerivedClass()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new Mock<ContentReference> {CallBase = true}.Object.Equals(It.IsAny<object>());
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void GetHashCode_ShouldThrowANotImplementedException_BecauseTheMethodMustBeImplementedInTheDerivedClass()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new Mock<ContentReference> {CallBase = true}.Object.GetHashCode();
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void GreaterThanOperator_IfBothParametersAreNotNull_ShouldCallCompareToOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.CompareTo(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object > Mock.Of<ContentReference>());
			contentReferenceMock.Verify(contentReference => contentReference.CompareTo(It.IsAny<object>()), Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GreaterThanOperator_IfBothParametersAreNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull((ContentReference) null > null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "firstContentReference")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GreaterThanOperator_IfTheFirstParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull(null > Mock.Of<ContentReference>());
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "firstContentReference")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GreaterThanOperator_IfTheSecondParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull(Mock.Of<ContentReference>() > null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "secondContentReference")
					throw;
			}
		}

		[TestMethod]
		public void InequalityOperator_IfBothParametersAreNull_ShouldReturnFalse()
		{
			Assert.IsFalse((ContentReference) null != null);
		}

		[TestMethod]
		public void InequalityOperator_IfBothParametersAreTheSameInstance_ShouldReturnFalse()
		{
			ContentReference contentReference = Mock.Of<ContentReference>();
#pragma warning disable 1718
			// ReSharper disable EqualExpressionComparison
			Assert.IsFalse(contentReference != contentReference);
			// ReSharper restore EqualExpressionComparison
#pragma warning restore 1718
		}

		[TestMethod]
		public void InequalityOperator_IfOneParametersIsNull_ShouldReturnTrue()
		{
			Assert.IsTrue(null != Mock.Of<ContentReference>());
			Assert.IsTrue(Mock.Of<ContentReference>() != null);
		}

		[TestMethod]
		public void InequalityOperator_IfTheParametersAreNotNullAndAreDifferentInstances_ShouldCallEqualsOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object != Mock.Of<ContentReference>()); // Will not throw a NotImplementedException because the method is not setup and CallBase is not set to true on the mock.
			contentReferenceMock.Verify(contentReference => contentReference.Equals(It.IsAny<object>()), Times.Once());
		}

		[TestMethod]
		public void LessThanOperator_IfBothParametersAreNotNull_ShouldCallCompareToOnTheFirstParameter()
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference>();
			contentReferenceMock.Verify(contentReference => contentReference.CompareTo(It.IsAny<object>()), Times.Never());
			Assert.IsNotNull(contentReferenceMock.Object < Mock.Of<ContentReference>());
			contentReferenceMock.Verify(contentReference => contentReference.CompareTo(It.IsAny<object>()), Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LessThanOperator_IfBothParametersAreNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull((ContentReference) null < null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "firstContentReference")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LessThanOperator_IfTheFirstParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull(null < Mock.Of<ContentReference>());
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "firstContentReference")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LessThanOperator_IfTheSecondParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				Assert.IsNotNull(Mock.Of<ContentReference>() < null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "secondContentReference")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void ToString_ShouldThrowANotImplementedException_BecauseTheMethodMustBeImplementedInTheDerivedClass()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new Mock<ContentReference> {CallBase = true}.Object.ToString();
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		#endregion
	}
}