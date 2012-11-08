﻿using System;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentReferenceTest
	{
		#region Fields

		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region Methods

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceProviderNameIsGreaterThanTheObjectParameterProviderName_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue), "1");
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.ProviderName = "2";
			Assert.AreEqual(1, 2.CompareTo(1));
			Assert.AreEqual(1, string.Compare("2", "1", StringComparison.Ordinal));
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceProviderNameIsLessThanTheObjectParameterProviderName_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue), "2");
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.ProviderName = "1";
			Assert.AreEqual(-1, 1.CompareTo(2));
			Assert.AreEqual(-1, string.Compare("1", "2", StringComparison.Ordinal));
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceWorkIdIsGreaterThanTheObjectParameterWorkId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue - 1), null);
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.WorkID = contentReference.WorkID + 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceWorkIdIsLessThanTheObjectParameterWorkId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(0, int.MaxValue), _random.Next(1, int.MaxValue), null);
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.WorkID = contentReference.WorkID - 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheInstanceIdIsGreaterThanTheObjectParameterId_ShouldReturnOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(0, int.MaxValue - 1), 0, null);
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.ID = contentReference.ID + 1;
			Assert.AreEqual(1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheInstanceIdIsLessThanTheObjectParameterId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = CreateContentReference(_random.Next(1, int.MaxValue), 0, null);
			ContentReference contentReference = CopyContentReference(contentReferenceToCompareWith);
			contentReference.ID = contentReference.ID - 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareTo_IfTheObjectParameterIsNotOfTypeContentReference_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new Mock<ContentReference> {CallBase = true}.Object.CompareTo(new object());
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareTo_IfTheObjectParameterIsNull_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new Mock<ContentReference> {CallBase = true}.Object.CompareTo(null);
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void CompareTo_IfTheObjectParameterIsTheSameAsTheInstance_ShouldReturnZero()
		{
			ContentReference contentReference = new Mock<ContentReference> {CallBase = true}.Object;
			Assert.AreEqual(0, contentReference.CompareTo(contentReference));
		}

		private static ContentReference CopyContentReference(ContentReference contentReferenceToCopy)
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> {CallBase = true};
			contentReferenceMock.SetupProperty(contentReference => contentReference.ID, contentReferenceToCopy.ID);
			contentReferenceMock.SetupProperty(contentReference => contentReference.WorkID, contentReferenceToCopy.WorkID);
			contentReferenceMock.SetupProperty(contentReference => contentReference.ProviderName, contentReferenceToCopy.ProviderName);
			return contentReferenceMock.Object;
		}

		private static ContentReference CreateContentReference(int id, int workId, string providerName)
		{
			Mock<ContentReference> contentReferenceMock = new Mock<ContentReference> {CallBase = true};
			contentReferenceMock.SetupProperty(contentReference => contentReference.ID, id);
			contentReferenceMock.SetupProperty(contentReference => contentReference.WorkID, workId);
			contentReferenceMock.SetupProperty(contentReference => contentReference.ProviderName, providerName);
			return contentReferenceMock.Object;
		}

		private static ContentReference CreateRandomContentReference()
		{
			return CreateContentReference(CreateRandomInteger(), CreateRandomInteger(), CreateRandomProviderName());
		}

		private static int CreateRandomInteger()
		{
			return _random.Next(int.MinValue, int.MaxValue);
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
		public void Equals_IfTheIdAndTheWorkIdAndTheProviderNameAreEqual_ShouldReturnTrue()
		{
			ContentReference contentReference = CreateRandomContentReference();
			Assert.IsTrue(contentReference.Equals(CopyContentReference(contentReference)));
		}

		[TestMethod]
		public void Equals_IfTheIdIsNotEqual_ShouldReturnFalse()
		{
			ContentReference contentReference = CreateRandomContentReference();
			ContentReference contentReferenceCopy = CopyContentReference(contentReference);
			contentReferenceCopy.ID = contentReferenceCopy.ID + 1;
			Assert.IsFalse(contentReference.Equals(contentReferenceCopy));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsNotOfTypeContentReference_ShouldReturnFalse()
		{
			Assert.IsFalse(new Mock<ContentReference> {CallBase = true}.Object.Equals(new object()));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(new Mock<ContentReference> {CallBase = true}.Object.Equals(null));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsTheSameInstance_ShouldReturnTrue()
		{
			ContentReference contentReference = new Mock<ContentReference> {CallBase = true}.Object;
			// ReSharper disable EqualExpressionComparison
			Assert.IsTrue(contentReference.Equals(contentReference));
			// ReSharper restore EqualExpressionComparison
		}

		[TestMethod]
		public void Equals_IfTheProviderNameIsNotEqual_ShouldReturnFalse()
		{
			ContentReference contentReference = CreateRandomContentReference();
			ContentReference contentReferenceCopy = CopyContentReference(contentReference);

			if(contentReferenceCopy.ProviderName == null)
				contentReferenceCopy.ProviderName = string.Empty;
			else if(contentReferenceCopy.ProviderName.Length == 0)
				contentReferenceCopy.ProviderName = " ";
			else if(contentReferenceCopy.ProviderName == " ")
				contentReferenceCopy.ProviderName = Guid.NewGuid().ToString();
			else
				contentReferenceCopy.ProviderName = null;

			Assert.IsFalse(contentReference.Equals(contentReferenceCopy));
		}

		[TestMethod]
		public void Equals_IfTheWorkIdIsNotEqual_ShouldReturnFalse()
		{
			ContentReference contentReference = CreateRandomContentReference();
			ContentReference contentReferenceCopy = CopyContentReference(contentReference);
			contentReferenceCopy.WorkID = contentReferenceCopy.WorkID + 1;
			Assert.IsFalse(contentReference.Equals(contentReferenceCopy));
		}

		[TestMethod]
		public void GetHashCode_IfTheProviderNameIsNotNull_ShouldReturnIdAddedWithWorkIdAddedWithTheHashCodeOfTheProviderName()
		{
			ContentReference contentReference = CreateRandomContentReference();
			contentReference.ProviderName = Guid.NewGuid().ToString();
			Assert.AreEqual(contentReference.ID + contentReference.WorkID + contentReference.ProviderName.GetHashCode(), contentReference.GetHashCode());
		}

		[TestMethod]
		public void GetHashCode_IfTheProviderNameIsNull_ShouldReturnIdAddedWithWorkId()
		{
			ContentReference contentReference = CreateRandomContentReference();
			contentReference.ProviderName = null;
			Assert.AreEqual(contentReference.ID + contentReference.WorkID, contentReference.GetHashCode());
		}

		[TestMethod]
		public void ToString_IfTheIdIsZeroAndTheWorkIdIsMinusOne_ShouldReturnAHyphen()
		{
			Assert.AreEqual("-", CreateContentReference(0, -1, "Test").ToString());
		}

		[TestMethod]
		public void ToString_IfTheIdIsZeroAndTheWorkIdIsNotMinusOne_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, CreateContentReference(0, 0, "Test").ToString());
		}

		#endregion
	}
}