using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using EPiServer;
using EPiServer.Core;
using EPiServer.Fakes;
using EPiServer.Web;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.ShimTests // ReSharper restore CheckNamespace
{
	[TestClass]
	public class DataFactoryWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastOrThrowTypeMismatchException_IfThePageDataParameterIsNotOfRequiredType_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).CastOrThrowTypeMismatchException<IContentData>(new PageData(), CreateRandomContentReference());
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastOrThrowTypeMismatchException_IfThePageDataParameterIsNull_ShouldReturnNull()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Assert.IsNull(new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).CastOrThrowTypeMismatchException<IContentData>(null, CreateRandomContentReference()));
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastOrThrowTypeMismatchException_IfThePageDataParameterIsOfRequiredType_ShouldReturnThePageDataParameterCasted()
		{
			using(ShimsContext.Create())
			{
				Mock<PageData> pageDataMock = new Mock<PageData>();
				pageDataMock.As<IContent>();
				PageData pageData = pageDataMock.Object;

				ShimDataFactory.StaticConstructor = () => { };
				IContent castedPageData = new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).CastOrThrowTypeMismatchException<IContent>(pageData, CreateRandomContentReference());
				Assert.AreEqual(pageData, castedPageData);
			}
		}

		[TestMethod]
		public void Constructor_IfTheDataFactoryParameterIsNotNull_ShouldSetTheDataFactoryProperty()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();
				Assert.AreEqual(dataFactory, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).DataFactory);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.EPiServer.Cms7.Abstractions.DataFactoryWrapper")]
		public void Constructor_IfThePermanentLinkMapperParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };

				try
				{
					// ReSharper disable ObjectCreationAsStatement
					new DataFactoryWrapper(new DataFactory(), null);
					// ReSharper restore ObjectCreationAsStatement
				}
				catch(ArgumentNullException argumentNullException)
				{
					if(argumentNullException.ParamName == "permanentLinkMapper")
						throw;
				}
			}
		}

		private static ContentReference CreateRandomContentReference()
		{
			int random = DateTime.Now.Second%3;

			switch(random)
			{
				case 0:
					return null;
				case 1:
					return ContentReference.EmptyReference;
				default:
					return new ContentReference(DateTime.Now.Millisecond + 1);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetChildren_Generic_WithFourParameters_IfGetChildrenWithPageReferenceParameterThrowsException_ShouldThrowTheException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new InvalidOperationException());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>(), DateTime.Now.Second, DateTime.Now.Millisecond);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetChildren_Generic_WithFourParameters_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).GetChildren<IContentData>(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null, Mock.Of<ILanguageSelector>(), DateTime.Now.Second, DateTime.Now.Millisecond);
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithFourParameters_ShouldCallGetChildrenWithPageReferenceParameter()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new PageDataCollection());
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>(), DateTime.Now.Second, DateTime.Now.Millisecond);
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithFourParameters_ShouldReturnAnEnumerableWOfRequestedType()
		{
			using(ShimsContext.Create())
			{
				PageDataCollection pageDataCollection = new PageDataCollection();

				int items = DateTime.Now.Millisecond;

				if(items%2 == 1)
					items++;

				for(int i = 0; i < items; i++)
				{
					Mock<PageData> pageDataMock = new Mock<PageData>();

					if(i%2 == 0)
						pageDataMock.As<IContentData>();

					pageDataCollection.Add(pageDataMock.Object);
				}

				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pageDataCollection);
				IEnumerable<IContentData> children = dataFactoryWrapperMock.Object.GetChildren<IContentData>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>(), DateTime.Now.Second, DateTime.Now.Millisecond);
				Assert.AreEqual(items/2, children.Count());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetChildren_Generic_WithOneParameter_IfGetChildrenWithPageReferenceParameterThrowsException_ShouldThrowTheException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>())).Throws(new InvalidOperationException());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetChildren_Generic_WithOneParameter_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).GetChildren<IContentData>(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null);
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithOneParameter_ShouldCallGetChildrenWithPageReferenceParameter()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>())).Returns(new PageDataCollection());
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>()), Times.Never());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond));
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>()), Times.Once());
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithOneParameter_ShouldReturnAnEnumerableWOfRequestedType()
		{
			using(ShimsContext.Create())
			{
				PageDataCollection pageDataCollection = new PageDataCollection();

				int items = DateTime.Now.Millisecond;

				if(items%2 == 1)
					items++;

				for(int i = 0; i < items; i++)
				{
					Mock<PageData> pageDataMock = new Mock<PageData>();

					if(i%2 == 0)
						pageDataMock.As<IContentData>();

					pageDataCollection.Add(pageDataMock.Object);
				}

				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>())).Returns(pageDataCollection);
				IEnumerable<IContentData> children = dataFactoryWrapperMock.Object.GetChildren<IContentData>(new ContentReference(DateTime.Now.Millisecond));
				Assert.AreEqual(items/2, children.Count());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void GetChildren_Generic_WithTwoParameters_IfGetChildrenWithPageReferenceParameterThrowsException_ShouldThrowTheException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Throws(new InvalidOperationException());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetChildren_Generic_WithTwoParameters_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).GetChildren<IContentData>(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null, Mock.Of<ILanguageSelector>());
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithTwoParameters_ShouldCallGetChildrenWithPageReferenceParameter()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Returns(new PageDataCollection());
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>()), Times.Never());
				dataFactoryWrapperMock.Object.GetChildren<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>()), Times.Once());
			}
		}

		[TestMethod]
		public void GetChildren_Generic_WithTwoParameters_ShouldReturnAnEnumerableWOfRequestedType()
		{
			using(ShimsContext.Create())
			{
				PageDataCollection pageDataCollection = new PageDataCollection();

				int items = DateTime.Now.Millisecond;

				if(items%2 == 1)
					items++;

				for(int i = 0; i < items; i++)
				{
					Mock<PageData> pageDataMock = new Mock<PageData>();

					if(i%2 == 0)
						pageDataMock.As<IContentData>();

					pageDataCollection.Add(pageDataMock.Object);
				}

				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetChildren(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Returns(pageDataCollection);
				IEnumerable<IContentData> children = dataFactoryWrapperMock.Object.GetChildren<IContentData>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
				Assert.AreEqual(items/2, children.Count());
			}
		}

		[TestMethod]
		public void GetChildren_WithFourParameters_ShouldCallGetChildrenWithFourParametersOfTheWrappedDataFactory()
		{
			using(ShimsContext.Create())
			{
				bool getChildrenIsCalled = false;
				PageReference pageLinkValue = null;
				ILanguageSelector selectorValue = null;
				int? startIndexValue = null;
				int? maxRowsValue = null;
				PageDataCollection pageDataCollection = new PageDataCollection();
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();

				new ShimDataFactory(dataFactory).GetChildrenPageReferenceILanguageSelectorInt32Int32 = delegate(PageReference pageLink, ILanguageSelector selector, int startIndex, int maxRows)
				{
					getChildrenIsCalled = true;
					pageLinkValue = pageLink;
					selectorValue = selector;
					startIndexValue = startIndex;
					maxRowsValue = maxRows;
					return pageDataCollection;
				};

				PageReference pageLinkParameter = new PageReference();
				ILanguageSelector selectorParameter = Mock.Of<ILanguageSelector>();
				int startIndexParameter = DateTime.Now.Second;
				int maxRowsParameter = DateTime.Now.Millisecond;

				Assert.IsFalse(getChildrenIsCalled);
				Assert.AreEqual(pageDataCollection, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).GetChildren(pageLinkParameter, selectorParameter, startIndexParameter, maxRowsParameter));
				Assert.IsTrue(getChildrenIsCalled);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
				Assert.AreEqual(selectorParameter, selectorValue);
				Assert.AreEqual(startIndexValue.Value, startIndexParameter);
				Assert.AreEqual(maxRowsValue.Value, maxRowsParameter);
			}
		}

		[TestMethod]
		public void GetChildren_WithOneParameter_ShouldCallGetChildrenWithOneParameterOfTheWrappedDataFactory()
		{
			using(ShimsContext.Create())
			{
				bool getChildrenIsCalled = false;
				PageReference pageLinkValue = null;
				PageDataCollection pageDataCollection = new PageDataCollection();
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();

				new ShimDataFactory(dataFactory).GetChildrenPageReference = delegate(PageReference pageLink)
				{
					getChildrenIsCalled = true;
					pageLinkValue = pageLink;
					return pageDataCollection;
				};

				PageReference pageLinkParameter = new PageReference();

				Assert.IsFalse(getChildrenIsCalled);
				Assert.AreEqual(pageDataCollection, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).GetChildren(pageLinkParameter));
				Assert.IsTrue(getChildrenIsCalled);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
			}
		}

		[TestMethod]
		public void GetChildren_WithTwoParameters_ShouldCallGetChildrenWithTwoParametersOfTheWrappedDataFactory()
		{
			using(ShimsContext.Create())
			{
				bool getChildrenIsCalled = false;
				PageReference pageLinkValue = null;
				ILanguageSelector selectorValue = null;
				PageDataCollection pageDataCollection = new PageDataCollection();
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();

				new ShimDataFactory(dataFactory).GetChildrenPageReferenceILanguageSelector = delegate(PageReference pageLink, ILanguageSelector selector)
				{
					getChildrenIsCalled = true;
					pageLinkValue = pageLink;
					selectorValue = selector;
					return pageDataCollection;
				};

				PageReference pageLinkParameter = new PageReference();
				ILanguageSelector selectorParameter = Mock.Of<ILanguageSelector>();

				Assert.IsFalse(getChildrenIsCalled);
				Assert.AreEqual(pageDataCollection, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).GetChildren(pageLinkParameter, selectorParameter));
				Assert.IsTrue(getChildrenIsCalled);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
				Assert.AreEqual(selectorParameter, selectorValue);
			}
		}

		[TestMethod]
		public void GetPage_WithOneParameter_ShouldCallGetPageWithOneParameterOfTheWrappedDataFactory()
		{
			using(ShimsContext.Create())
			{
				bool getPageIsCalled = false;
				PageReference pageLinkValue = null;
				PageData pageData = new PageData();
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();

				new ShimDataFactory(dataFactory).GetPagePageReference = delegate(PageReference pageLink)
				{
					getPageIsCalled = true;
					pageLinkValue = pageLink;
					return pageData;
				};

				PageReference pageLinkParameter = new PageReference();

				Assert.IsFalse(getPageIsCalled);
				Assert.AreEqual(pageData, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).GetPage(pageLinkParameter));
				Assert.IsTrue(getPageIsCalled);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
			}
		}

		[TestMethod]
		public void GetPage_WithTwoParameters_ShouldCallGetPageWithTwoParametersOfTheWrappedDataFactory()
		{
			using(ShimsContext.Create())
			{
				bool getPageIsCalled = false;
				PageReference pageLinkValue = null;
				ILanguageSelector selectorValue = null;
				PageData pageData = new PageData();
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();

				new ShimDataFactory(dataFactory).GetPagePageReferenceILanguageSelector = delegate(PageReference pageLink, ILanguageSelector selector)
				{
					getPageIsCalled = true;
					pageLinkValue = pageLink;
					selectorValue = selector;
					return pageData;
				};

				PageReference pageLinkParameter = new PageReference();
				ILanguageSelector selectorParameter = Mock.Of<ILanguageSelector>();

				Assert.IsFalse(getPageIsCalled);
				Assert.AreEqual(pageData, new DataFactoryWrapper(dataFactory, Mock.Of<IPermanentLinkMapper>()).GetPage(pageLinkParameter, selectorParameter));
				Assert.IsTrue(getPageIsCalled);
				Assert.AreEqual(pageLinkParameter, pageLinkValue);
				Assert.AreEqual(selectorParameter, selectorValue);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(PageNotFoundException))]
		public void Get_Generic_WithContentReferenceAndLanguageSelectorParameters_IfGetChildrenWithPageReferenceParameterThrowsException_ShouldThrowTheException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Throws(new PageNotFoundException(Mock.Of<PageReference>()));
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Get_Generic_WithContentReferenceAndLanguageSelectorParameters_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).Get<IContentData>(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null, Mock.Of<ILanguageSelector>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void Get_Generic_WithContentReferenceAndLanguageSelectorParameters_IfThePageDataReturnedByGetPageIsNotOfRequiredType_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Returns(new PageData());
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
			}
		}

		[TestMethod]
		public void Get_Generic_WithContentReferenceAndLanguageSelectorParameters_IfThePageDataReturnedByGetPageIsOfRequiredType_ShouldReturnThePageDataCasted()
		{
			using(ShimsContext.Create())
			{
				Mock<PageData> pageDataMock = new Mock<PageData>();
				pageDataMock.As<IContent>();
				PageData pageData = pageDataMock.Object;
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Returns(pageData);
				IContent content = dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
				Assert.AreEqual(pageData, content);
			}
		}

		[TestMethod]
		public void Get_Generic_WithContentReferenceAndLanguageSelectorParameters_ShouldCallGetPageWithPageReferenceAndLanguageSelectorParameters()
		{
			using(ShimsContext.Create())
			{
				Mock<PageData> pageDataMock = new Mock<PageData>();
				pageDataMock.As<IContent>();
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>())).Returns(pageDataMock.Object);
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>()), Times.Never());
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond), Mock.Of<ILanguageSelector>());
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>(), It.IsAny<ILanguageSelector>()), Times.Once());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(PageNotFoundException))]
		public void Get_Generic_WithContentReferenceParameter_IfGetChildrenWithPageReferenceParameterThrowsException_ShouldThrowTheException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>())).Throws(new PageNotFoundException(Mock.Of<PageReference>()));
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Get_Generic_WithContentReferenceParameter_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).Get<IContentData>(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void Get_Generic_WithContentReferenceParameter_IfThePageDataReturnedByGetPageIsNotOfRequiredType_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>())).Returns(new PageData());
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond));
			}
		}

		[TestMethod]
		public void Get_Generic_WithContentReferenceParameter_IfThePageDataReturnedByGetPageIsOfRequiredType_ShouldReturnThePageDataCasted()
		{
			using(ShimsContext.Create())
			{
				Mock<PageData> pageDataMock = new Mock<PageData>();
				pageDataMock.As<IContent>();
				PageData pageData = pageDataMock.Object;
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>())).Returns(pageData);
				IContent content = dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond));
				Assert.AreEqual(pageData, content);
			}
		}

		[TestMethod]
		public void Get_Generic_WithContentReferenceParameter_ShouldCallGetPageWithPageReferenceParameter()
		{
			using(ShimsContext.Create())
			{
				Mock<PageData> pageDataMock = new Mock<PageData>();
				pageDataMock.As<IContent>();
				ShimDataFactory.StaticConstructor = () => { };
				Mock<DataFactoryWrapper> dataFactoryWrapperMock = new Mock<DataFactoryWrapper>(new object[] {new DataFactory(), Mock.Of<IPermanentLinkMapper>()}) {CallBase = true};
				dataFactoryWrapperMock.Setup(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>())).Returns(pageDataMock.Object);
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>()), Times.Never());
				dataFactoryWrapperMock.Object.Get<IContent>(new ContentReference(DateTime.Now.Millisecond));
				dataFactoryWrapperMock.Verify(dataFactoryWrapper => dataFactoryWrapper.GetPage(It.IsAny<PageReference>()), Times.Once());
			}
		}

		[TestMethod]
		public void ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty_IfTheContentLinkParameterIsNotNullOrEmpty_ShouldNotThrowAnArgumentNullException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(new ContentReference(DateTime.Now.Millisecond));
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty_IfTheContentLinkParameterIsNullOrEmpty_ShouldThrowAnArgumentNullException()
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;

			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");

			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				try
				{
					new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(DateTime.Now.Second%2 == 0 ? ContentReference.EmptyReference : null);
				}
				catch(ArgumentNullException argumentNullException)
				{
					if(argumentNullException.ParamName == "contentLink" && argumentNullException.Message == "The provided content link does not have a value." + Environment.NewLine + "Parameter name: contentLink")
						throw;
				}
			}

			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfTheActualTypeParameterIsNull_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				try
				{
					new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowTypeMismatchException(new ContentReference(1), null, typeof(string));
				}
				catch(TypeMismatchException typeMismatchException)
				{
					if(typeMismatchException.Message == "Content with id '1' is of type '' which does not inherit required type 'System.String'")
						throw;
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfTheContentLinkParameterIsNull_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				try
				{
					new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowTypeMismatchException(null, typeof(object), typeof(string));
				}
				catch(TypeMismatchException typeMismatchException)
				{
					if(typeMismatchException.Message == "Content with id '' is of type 'System.Object' which does not inherit required type 'System.String'")
						throw;
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfTheRequiredTypeParameterIsNull_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				try
				{
					new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowTypeMismatchException(new ContentReference(1), typeof(object), null);
				}
				catch(TypeMismatchException typeMismatchException)
				{
					if(typeMismatchException.Message == "Content with id '1' is of type 'System.Object' which does not inherit required type ''")
						throw;
				}
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_ShouldThrowATypeMismatchException()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				try
				{
					new DataFactoryWrapper(new DataFactory(), Mock.Of<IPermanentLinkMapper>()).ThrowTypeMismatchException(new ContentReference(1), typeof(object), typeof(string));
				}
				catch(TypeMismatchException typeMismatchException)
				{
					if(typeMismatchException.Message == "Content with id '1' is of type 'System.Object' which does not inherit required type 'System.String'")
						throw;
				}
			}
		}

		#endregion
	}
}