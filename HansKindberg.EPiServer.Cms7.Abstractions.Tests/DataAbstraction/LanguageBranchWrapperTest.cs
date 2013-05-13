using System;
using HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Tests.DataAbstraction // ReSharper restore CheckNamespace
{
	[TestClass]
	public class LanguageBranchWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Save_IfTheLanguageBranchParameterValueIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new LanguageBranchWrapper().Save(null);
			}
			catch(ArgumentNullException argumentNullException)
			{
				if(argumentNullException.ParamName == "languageBranch")
					throw;
			}
		}

		#endregion
	}
}