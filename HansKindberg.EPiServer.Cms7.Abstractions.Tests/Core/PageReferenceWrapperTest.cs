using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			PageReference pageReference = new PageReference(CreateRandomInteger(), CreateRandomInteger(), CreateRandomRemoteSite(), CreateRandomBoolean());

			if(CreateRandomBoolean())
				pageReference.MakeReadOnly();

			return pageReference;
		}

		private static string CreateRandomRemoteSite()
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
		public void GetPublishedOrLatest_ShouldReturnIsAnyVersionOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.IsAnyVersion(), new PageReferenceWrapper(pageReference).GetPublishedOrLatest);
		}

		[TestMethod]
		public void Id_ShouldReturnTheIdOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.ID, new PageReferenceWrapper(pageReference).ID);
		}

		[TestMethod]
		public void IsExternalProvider_ShouldReturnIsRemoteOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.IsRemote(), new PageReferenceWrapper(pageReference).IsExternalProvider);
		}

		[TestMethod]
		public void IsReadOnly_ShouldReturnIsReadOnlyOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.IsReadOnly, new PageReferenceWrapper(pageReference).IsReadOnly);
		}

		[TestMethod]
		public void ProviderName_ShouldReturnTheRemoteSiteOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.RemoteSite, new PageReferenceWrapper(pageReference).ProviderName);
		}

		[TestMethod]
		public void WorkId_ShouldReturnTheWorkIdOfTheWrappedPageReference()
		{
			PageReference pageReference = CreateRandomPageReference();
			Assert.AreEqual(pageReference.WorkID, new PageReferenceWrapper(pageReference).WorkID);
		}

		#endregion
	}
}