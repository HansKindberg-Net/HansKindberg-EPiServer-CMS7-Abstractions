using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EPiServer.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.Tests.Core
{
	[TestClass]
	public class ContentDataTest
	{
		#region Methods

		[TestMethod]
		public void Constructor_WithNoParameters_ShouldSetPropertiesToAnEmptyPropertyDataCollection()
		{
			Assert.IsFalse(new ContentDataTestContentData().Properties.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "EPiServer.Tests.Core.ContentDataTestContentData")]
		public void Constructor_WithPropertyDataCollectionParameter_IfThePropertyDataCollectionParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new ContentDataTestContentData(null);
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "properties")
					throw;
			}
		}

		[TestMethod]
		public void Constructor_WithPropertyDataCollectionParameter_ShouldSetProperties()
		{
			PropertyDataCollection properties = new PropertyDataCollection();
			Assert.AreEqual(properties, new ContentDataTestContentData(properties).Properties);
		}

		#endregion
	}

	internal class ContentDataTestContentData : ContentData
	{
		#region Constructors

		public ContentDataTestContentData() {}
		public ContentDataTestContentData(PropertyDataCollection properties) : base(properties) {}

		#endregion

		#region Properties

		public new virtual PropertyDataCollection Properties
		{
			get { return base.Properties; }
			set { base.Properties = value; }
		}

		#endregion
	}
}