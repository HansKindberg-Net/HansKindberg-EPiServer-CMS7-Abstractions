using System;
using EPiServer.DataAbstraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.Tests.DataAbstraction
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