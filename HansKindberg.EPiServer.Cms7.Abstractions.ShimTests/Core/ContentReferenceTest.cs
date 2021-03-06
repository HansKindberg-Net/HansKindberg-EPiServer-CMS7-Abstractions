﻿using System;
using EPiServer.Core;
using EPiServer.Core.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.ShimTests.Core
{
	[TestClass]
	public class ContentReferenceTest
	{
		//[TestMethod]
		//public void CompareTo_IfTheObjectParameterIsOfTypePageReferenceWrapperAndIsNotSameAsTheInstance_ShouldCallCompareToOfTheWrappedPageReference()
		//{
		//	using (ShimsContext.Create())
		//	{
		//		bool compareToIsCalled = false;
		//		PageReference pageReference = CreateRandomPageReference();
		//		ShimPageReference.StaticConstructor = () => { };
		//		// ReSharper disable ObjectCreationAsStatement
		//		new ShimPageReference(pageReference)
		//		{
		//			CompareToObject = delegate
		//			{
		//				compareToIsCalled = true;
		//				return int.MinValue;
		//			}
		//		};
		//		// ReSharper restore ObjectCreationAsStatement
		//		Assert.IsNotNull(new PageReferenceWrapper(pageReference).CompareTo(CreateRandomPageReferenceWrapper()));
		//		Assert.IsTrue(compareToIsCalled);
		//	}
		//}
		//private static PageReferenceWrapper CreateRandomPageReferenceWrapper()
		//{
		//	return new PageReferenceWrapper(CreateRandomPageReference());
		//}
		//[TestMethod]
		//public void ParseReference_ShouldCallStaticParseOfPageReference()
		//{
		//	using (ShimsContext.Create())
		//	{
		//		bool parseIsCalled = false;
		//		PageReference pageReference = CreateRandomPageReference();
		//		ShimPageReference.StaticConstructor = () => { };
		//		ShimPageReference.ParseString = delegate
		//		{
		//			parseIsCalled = true;
		//			return CreateRandomPageReference();
		//		};
		//		// ReSharper disable ObjectCreationAsStatement
		//		new PageReferenceWrapper(pageReference).ParseReference(string.Empty);
		//		// ReSharper restore ObjectCreationAsStatement
		//		Assert.IsTrue(parseIsCalled);
		//	}
		//}

		#region Fields

		private static readonly Random _random = new Random(DateTime.Now.Millisecond);

		#endregion

		#region Methods

		[TestMethod]
		public void CompareToIgnoreWorkId_ShouldCallCompareToIgnoreWorkIdOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool compareToIgnoreWorkIdIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();
				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						CompareToIgnoreWorkIDPageReference = delegate
						{
							compareToIgnoreWorkIdIsCalled = true;
							return true;
						}
					};

				new ContentReference(pageReference).CompareToIgnoreWorkID(new ContentReference(CreateRandomPageReference()));
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(compareToIgnoreWorkIdIsCalled);
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
		public void CreateReferenceWithoutVersion_ShouldCallCreateReferenceToPublishedPageOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool createReferenceToPublishedPageIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						CreateReferenceToPublishedPage = delegate
						{
							createReferenceToPublishedPageIsCalled = true;
							return CreateRandomPageReference();
						}
					};

				new ContentReference(pageReference).CreateReferenceWithoutVersion();
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(createReferenceToPublishedPageIsCalled);
			}
		}

		[TestMethod]
		public void CreateWritableClone_ShouldCallCreateWritableCloneOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool createWritableCloneIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						CreateWritableClone = delegate
						{
							createWritableCloneIsCalled = true;
							return CreateRandomPageReference();
						}
					};

				new ContentReference(pageReference).CreateWritableClone();
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(createWritableCloneIsCalled);
			}
		}

		[TestMethod]
		public void GetPublishedOrLatest_ShouldReturnIsAnyVersionOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool isAnyVersionIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						IsAnyVersion = delegate
						{
							isAnyVersionIsCalled = true;
							return true;
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).GetPublishedOrLatest);
				Assert.IsTrue(isAnyVersionIsCalled);
			}
		}

		[TestMethod]
		public void Id_Get_ShouldReturnTheIdOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool idIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						IDGet = delegate
						{
							idIsCalled = true;
							return int.MinValue;
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).ID);
				Assert.IsTrue(idIsCalled);
			}
		}

		[TestMethod]
		public void Id_Set_ShouldSetTheIdOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool idIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						IDSetInt32 = delegate { idIsCalled = true; }
					};

				new ContentReference(pageReference) {ID = int.MinValue};
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(idIsCalled);
			}
		}

		[TestMethod]
		public void IsExternalProvider_ShouldReturnIsRemoteOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool isRemoteIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						IsRemote = delegate
						{
							isRemoteIsCalled = true;
							return true;
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).IsExternalProvider);
				Assert.IsTrue(isRemoteIsCalled);
			}
		}

		[TestMethod]
		public void IsReadOnly_ShouldReturnIsReadOnlyOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool isReadOnlyIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						IsReadOnlyGet = delegate
						{
							isReadOnlyIsCalled = true;
							return true;
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).IsReadOnly);
				Assert.IsTrue(isReadOnlyIsCalled);
			}
		}

		[TestMethod]
		public void MakeReadOnly_ShouldCallMakeReadOnlyOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool makeReadOnlyIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						MakeReadOnly = delegate { makeReadOnlyIsCalled = true; }
					};

				new ContentReference(pageReference).MakeReadOnly();
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(makeReadOnlyIsCalled);
			}
		}

		[TestMethod]
		public void ProviderName_Get_ShouldReturnTheRemoteSiteOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool remoteSiteIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						RemoteSiteGet = delegate
						{
							remoteSiteIsCalled = true;
							return "Test";
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).ProviderName);
				Assert.IsTrue(remoteSiteIsCalled);
			}
		}

		[TestMethod]
		public void ProviderName_Set_ShouldSetTheRemoteSiteOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool remoteSiteIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						RemoteSiteSetString = delegate { remoteSiteIsCalled = true; }
					};

				new ContentReference(pageReference) {ProviderName = CreateRandomProviderName()};
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(remoteSiteIsCalled);
			}
		}

		[TestMethod]
		public void WorkId_Get_ShouldReturnTheWorkIdOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool workIdIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						WorkIDGet = delegate
						{
							workIdIsCalled = true;
							return int.MinValue;
						}
					};
				// ReSharper restore ObjectCreationAsStatement

				Assert.IsNotNull(new ContentReference(pageReference).WorkID);
				Assert.IsTrue(workIdIsCalled);
			}
		}

		[TestMethod]
		public void WorkId_Set_ShouldSetTheWorkIdOfTheWrappedPageReference()
		{
			using(ShimsContext.Create())
			{
				bool workIdIsCalled = false;
				PageReference pageReference = CreateRandomPageReference();

				ShimPageReference.StaticConstructor = () => { };
				// ReSharper disable ObjectCreationAsStatement
				new ShimPageReference(pageReference)
					{
						WorkIDSetInt32 = delegate { workIdIsCalled = true; }
					};

				new ContentReference(pageReference) {WorkID = int.MinValue};
				// ReSharper restore ObjectCreationAsStatement
				Assert.IsTrue(workIdIsCalled);
			}
		}

		#endregion
	}
}