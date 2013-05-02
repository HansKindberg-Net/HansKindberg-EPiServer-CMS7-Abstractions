using System;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue), "1");
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.ProviderName = "2";
			Assert.AreEqual(1, 2.CompareTo(1));
			Assert.AreEqual(1, string.Compare("2", "1", StringComparison.Ordinal));
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceProviderNameIsLessThanTheObjectParameterProviderName_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue), "2");
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.ProviderName = "1";
			Assert.AreEqual(-1, 1.CompareTo(2));
			Assert.AreEqual(-1, string.Compare("1", "2", StringComparison.Ordinal));
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceWorkIdIsGreaterThanTheObjectParameterWorkId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(0, int.MaxValue), _random.Next(0, int.MaxValue - 1));
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.WorkID = contentReference.WorkID + 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheIdsAreEqualAndTheInstanceWorkIdIsLessThanTheObjectParameterWorkId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(0, int.MaxValue), _random.Next(1, int.MaxValue));
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.WorkID = contentReference.WorkID - 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheInstanceIdIsGreaterThanTheObjectParameterId_ShouldReturnOne()
		{
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(0, int.MaxValue - 1));
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.ID = contentReference.ID + 1;
			Assert.AreEqual(1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		public void CompareTo_IfTheInstanceIdIsLessThanTheObjectParameterId_ShouldReturnMinusOne()
		{
			ContentReference contentReferenceToCompareWith = new ContentReference(_random.Next(1, int.MaxValue));
			ContentReference contentReference = contentReferenceToCompareWith.Copy();
			contentReference.ID = contentReference.ID - 1;
			Assert.AreEqual(-1, contentReference.CompareTo(contentReferenceToCompareWith));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareTo_IfTheObjectParameterIsNotOfTypeContentReference_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new ContentReference().CompareTo(new object());
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CompareTo_IfTheObjectParameterIsNull_ShouldThrowAnArgumentException()
		{
			// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			new ContentReference().CompareTo(null);
			// ReSharper restore ReturnValueOfPureMethodIsNotUsed
		}

		[TestMethod]
		public void CompareTo_IfTheObjectParameterIsTheSameAsTheInstance_ShouldReturnZero()
		{
			ContentReference contentReference = new ContentReference();
			Assert.AreEqual(0, contentReference.CompareTo(contentReference));
		}

		private static ContentReference CreateContentReference(int contentId, int versionId, string providerName)
		{
			return new ContentReference(contentId, versionId, providerName);
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
			Assert.IsTrue(contentReference.Equals(contentReference.Copy()));
		}

		[TestMethod]
		public void Equals_IfTheIdIsNotEqual_ShouldReturnFalse()
		{
			ContentReference contentReference = CreateRandomContentReference();
			ContentReference contentReferenceCopy = contentReference.Copy();
			contentReferenceCopy.ID = contentReferenceCopy.ID + 1;
			Assert.IsFalse(contentReference.Equals(contentReferenceCopy));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsNotOfTypeContentReference_ShouldReturnFalse()
		{
			Assert.IsFalse(new ContentReference().Equals(new object()));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsNull_ShouldReturnFalse()
		{
			Assert.IsFalse(new ContentReference().Equals(null));
		}

		[TestMethod]
		public void Equals_IfTheObjectParameterIsTheSameInstance_ShouldReturnTrue()
		{
			ContentReference contentReference = new ContentReference();
			// ReSharper disable EqualExpressionComparison
			Assert.IsTrue(contentReference.Equals(contentReference));
			// ReSharper restore EqualExpressionComparison
		}

		[TestMethod]
		public void Equals_IfTheProviderNameIsNotEqual_ShouldReturnFalse()
		{
			ContentReference contentReference = CreateRandomContentReference();
			ContentReference contentReferenceCopy = contentReference.Copy();

			if(contentReferenceCopy.ProviderName == null)
				contentReferenceCopy.ProviderName = "string.Emtpy"; // Setting it to an empty string will set it to null internally.
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
			ContentReference contentReferenceCopy = contentReference.Copy();
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
		public void ProviderName_Set_IfTheValueIsAWhiteSpace_ShouldSetItToAWhiteSpace()
		{
			ContentReference contentReference = new ContentReference {ProviderName = " "};
			Assert.AreEqual(" ", contentReference.ProviderName);
		}

		[TestMethod]
		public void ProviderName_Set_IfTheValueIsAnEmptyString_ShouldSetItToNull()
		{
			ContentReference contentReference = new ContentReference {ProviderName = string.Empty};
			Assert.IsNull(contentReference.ProviderName);
		}

		#endregion
	}
}