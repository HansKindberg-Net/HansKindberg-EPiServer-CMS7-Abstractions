using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.Core // ReSharper restore CheckNamespace
{
	[TestClass]
	public class PageDataCasterTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastToContentOrThrowTypeMismatchException_IfThePageDataParameterIsNotOfRequiredType_ShouldThrowATypeMismatchException()
		{
			new PageDataCaster().CastToContentOrThrowTypeMismatchException<IContentData>(new PageData());
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastToContentOrThrowTypeMismatchException_IfThePageDataParameterIsNull_ShouldReturnNull()
		{
			Assert.IsNull(new PageDataCaster().CastToContentOrThrowTypeMismatchException<IContentData>(null));
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		public void CastToContentOrThrowTypeMismatchException_IfThePageDataParameterIsOfRequiredType_ShouldReturnThePageDataParameterCasted()
		{
			Mock<PageData> pageDataMock = new Mock<PageData>();
			pageDataMock.As<IContent>();
			PageData pageData = pageDataMock.Object;

			IContent castedPageData = new PageDataCaster().CastToContentOrThrowTypeMismatchException<IContent>(pageData);
			Assert.AreEqual(pageData, castedPageData);
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfTheActualTypeParameterIsNull_ShouldThrowATypeMismatchException()
		{
			try
			{
				new PageDataCaster().ThrowTypeMismatchException(new PageReference(1), null, typeof(string));
			}
			catch(TypeMismatchException typeMismatchException)
			{
				if(typeMismatchException.Message == "Content with id \"1\" is of type \"\" which does not inherit required type \"System.String\".")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfThePageLinkParameterIsNull_ShouldThrowATypeMismatchException()
		{
			try
			{
				new PageDataCaster().ThrowTypeMismatchException(null, typeof(object), typeof(string));
			}
			catch(TypeMismatchException typeMismatchException)
			{
				if(typeMismatchException.Message == "Content with id \"\" is of type \"System.Object\" which does not inherit required type \"System.String\".")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_IfTheRequiredTypeParameterIsNull_ShouldThrowATypeMismatchException()
		{
			try
			{
				new PageDataCaster().ThrowTypeMismatchException(new PageReference(1), typeof(object), null);
			}
			catch(TypeMismatchException typeMismatchException)
			{
				if(typeMismatchException.Message == "Content with id \"1\" is of type \"System.Object\" which does not inherit required type \"\".")
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TypeMismatchException))]
		public void ThrowTypeMismatchException_ShouldThrowATypeMismatchException()
		{
			try
			{
				new PageDataCaster().ThrowTypeMismatchException(new PageReference(1, 10, "Test"), typeof(object), typeof(string));
			}
			catch(TypeMismatchException typeMismatchException)
			{
				if(typeMismatchException.Message == "Content with id \"1_10_Test\" is of type \"System.Object\" which does not inherit required type \"System.String\".")
					throw;
			}
		}

		#endregion
	}
}