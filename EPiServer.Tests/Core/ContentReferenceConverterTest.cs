using System.ComponentModel;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentReferenceConverterTest
	{
		#region Methods

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeContentReference_ShouldReturnFalse()
		{
			Assert.IsFalse(new ContentReferenceConverter<ContentReference>().CanConvertFrom(Mock.Of<ITypeDescriptorContext>(), typeof(ContentReference)));
		}

		[TestMethod]
		public void CanConvertFrom_IfTheSourceTypeParameterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(new ContentReferenceConverter<ContentReference>().CanConvertFrom(Mock.Of<ITypeDescriptorContext>(), typeof(string)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsOfTypeContentReference_ShouldReturnFalse()
		{
			Assert.IsFalse(new ContentReferenceConverter<ContentReference>().CanConvertTo(Mock.Of<ITypeDescriptorContext>(), typeof(ContentReference)));
		}

		[TestMethod]
		public void CanConvertTo_IfTheDestinationTypeParameterIsOfTypeString_ShouldReturnTrue()
		{
			Assert.IsTrue(new ContentReferenceConverter<ContentReference>().CanConvertTo(Mock.Of<ITypeDescriptorContext>(), typeof(string)));
		}

		#endregion
	}
}