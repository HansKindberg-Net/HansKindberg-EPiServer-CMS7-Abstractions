using EPiServer;
using EPiServer.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.ShimTests // ReSharper restore CheckNamespace
{
	[TestClass]
	public class DataFactoryWrapperTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_IfTheDataFactoryParameterIsNotNull_ShouldSetTheDataFactoryProperty()
		{
			using(ShimsContext.Create())
			{
				ShimDataFactory.StaticConstructor = () => { };
				DataFactory dataFactory = new DataFactory();
				Assert.AreEqual(dataFactory, new DataFactoryWrapper(dataFactory).DataFactory);
			}
		}

		#endregion
	}
}