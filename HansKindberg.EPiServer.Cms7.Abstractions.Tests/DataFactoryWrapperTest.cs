﻿using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Web;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests // ReSharper restore CheckNamespace
{
	[TestClass]
	public class DataFactoryWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.EPiServer.Cms7.Abstractions.DataFactoryWrapper")]
		public void Constructor_IfTheDataFactoryParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				// ReSharper disable ObjectCreationAsStatement
				new DataFactoryWrapper(null, Mock.Of<IPageDataCaster>(), Mock.Of<IPermanentLinkMapper>());
				// ReSharper restore ObjectCreationAsStatement
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "dataFactory")
					throw;
			}
		}

		#endregion
	}
}