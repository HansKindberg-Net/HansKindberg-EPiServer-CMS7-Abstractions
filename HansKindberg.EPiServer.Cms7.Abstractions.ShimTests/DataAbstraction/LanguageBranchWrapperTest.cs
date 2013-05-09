using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPiServer.ShimTests.DataAbstraction
{
	[TestClass]
	public class LanguageBranchWrapperTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "EPiServer.DataAbstraction.LanguageBranch.#ctor")]
		public void Delete_ShouldCallLoadAndDeleteOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool loadIsCalled = false;
				bool deleteIsCalled = false;
				int? loadLanguageBranchIdValue = null;
				LanguageBranch loadedLanguageBranchValue = new LanguageBranch();
				LanguageBranch deleteLanguageBranchValue = null;

				ShimLanguageBranch.LoadInt32 = delegate(int languageBranchId)
				{
					loadLanguageBranchIdValue = languageBranchId;
					loadIsCalled = true;
					return loadedLanguageBranchValue;
				};

				ShimLanguageBranch.AllInstances.Delete = delegate(LanguageBranch languageBranch)
				{
					deleteLanguageBranchValue = languageBranch;
					deleteIsCalled = true;
				};

				int languageBranchIdParameter = DateTime.Now.Second;

				Assert.IsFalse(loadIsCalled);
				Assert.IsFalse(deleteIsCalled);

				new LanguageBranchWrapper().Delete(languageBranchIdParameter);

				Assert.IsTrue(loadIsCalled);
				Assert.IsTrue(deleteIsCalled);

				Assert.AreEqual(loadLanguageBranchIdValue.Value, languageBranchIdParameter);
				Assert.AreEqual(loadedLanguageBranchValue, deleteLanguageBranchValue);
			}
		}

		[TestMethod]
		public void ListAll_ShouldCallListAllOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool listAllIsCalled = false;
				LanguageBranchCollection listAllValue = new LanguageBranchCollection();

				ShimLanguageBranch.ListAll = delegate
				{
					listAllIsCalled = true;
					return listAllValue;
				};

				Assert.IsFalse(listAllIsCalled);
				Assert.AreEqual(listAllValue, new LanguageBranchWrapper().ListAll());
				Assert.IsTrue(listAllIsCalled);
			}
		}

		[TestMethod]
		public void ListEnabled_ShouldCallListEnabledOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool listEnabledIsCalled = false;
				LanguageBranchCollection listEnabledValue = new LanguageBranchCollection();

				ShimLanguageBranch.ListEnabled = delegate
				{
					listEnabledIsCalled = true;
					return listEnabledValue;
				};

				Assert.IsFalse(listEnabledIsCalled);
				Assert.AreEqual(listEnabledValue, new LanguageBranchWrapper().ListEnabled());
				Assert.IsTrue(listEnabledIsCalled);
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "EPiServer.DataAbstraction.LanguageBranch.#ctor")]
		public void LoadFirstEnabledBranch_ShouldCallLoadFirstEnabledBranchOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool loadFirstEnabledBranchIsCalled = false;
				LanguageBranch loadFirstEnabledBranchValue = new LanguageBranch();

				ShimLanguageBranch.LoadFirstEnabledBranch = delegate
				{
					loadFirstEnabledBranchIsCalled = true;
					return loadFirstEnabledBranchValue;
				};

				Assert.IsFalse(loadFirstEnabledBranchIsCalled);
				Assert.AreEqual(loadFirstEnabledBranchValue, new LanguageBranchWrapper().LoadFirstEnabledBranch());
				Assert.IsTrue(loadFirstEnabledBranchIsCalled);
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "EPiServer.DataAbstraction.LanguageBranch.#ctor")]
		public void Load_WithCultureInfoParameter_ShouldCallLoadWithCultureInfoParameterOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool loadIsCalled = false;
				LanguageBranch loadedLanguageBranch = new LanguageBranch();
				CultureInfo cultureValue = null;

				ShimLanguageBranch.LoadCultureInfo = delegate(CultureInfo culture)
				{
					loadIsCalled = true;
					cultureValue = culture;
					return loadedLanguageBranch;
				};

				CultureInfo cultureParameter = CultureInfo.InvariantCulture;

				Assert.IsFalse(loadIsCalled);
				Assert.AreEqual(loadedLanguageBranch, new LanguageBranchWrapper().Load(cultureParameter));
				Assert.IsTrue(loadIsCalled);
				Assert.AreEqual(cultureValue, cultureParameter);
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "EPiServer.DataAbstraction.LanguageBranch.#ctor")]
		public void Load_WithIntegerParameter_ShouldCallLoadWithIntegerParameterOfTheWrappedLanguageBranch()
		{
			using(ShimsContext.Create())
			{
				bool loadIsCalled = false;
				LanguageBranch loadedLanguageBranch = new LanguageBranch();
				int? idValue = null;

				ShimLanguageBranch.LoadInt32 = delegate(int id)
				{
					loadIsCalled = true;
					idValue = id;
					return loadedLanguageBranch;
				};

				int idParameter = DateTime.Now.Second;

				Assert.IsFalse(loadIsCalled);
				Assert.AreEqual(loadedLanguageBranch, new LanguageBranchWrapper().Load(idParameter));
				Assert.IsTrue(loadIsCalled);
				Assert.AreEqual(idValue.Value, idParameter);
			}
		}

		[TestMethod]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "EPiServer.DataAbstraction.LanguageBranch.#ctor")]
		public void Save_ShouldCallSaveOfTheLanguageBranchParameter()
		{
			using(ShimsContext.Create())
			{
				bool saveIsCalled = false;
				LanguageBranch languageBranchValue = null;
				LanguageBranch languageBranchParameter = new LanguageBranch();

				// ReSharper disable ObjectCreationAsStatement

				new ShimLanguageBranch(languageBranchParameter)
					{
						Save = delegate
						{
							saveIsCalled = true;
							languageBranchValue = languageBranchParameter;
						}
					};

				// ReSharper restore ObjectCreationAsStatement

				Assert.IsFalse(saveIsCalled);
				new LanguageBranchWrapper().Save(languageBranchParameter);
				Assert.IsTrue(saveIsCalled);
				Assert.AreEqual(languageBranchParameter, languageBranchValue);
			}
		}

		#endregion
	}
}