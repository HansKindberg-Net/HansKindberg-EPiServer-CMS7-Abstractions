using System;
using EPiServer.Core;
using EPiServer.Core.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EPiServer.ShimTests.Core
{
	[TestClass]
	public class LanguageSelectorFactoryTest
	{
		#region Methods

		[TestMethod]
		public void AutoDetect_ShouldCallAutoDetectOfTheWrappedLanguageSelector()
		{
			using(ShimsContext.Create())
			{
				bool autoDetectIsCalled = false;
				ILanguageSelector languageSelector = Mock.Of<ILanguageSelector>();

				ShimLanguageSelector.AutoDetect = delegate
				{
					autoDetectIsCalled = true;
					return languageSelector;
				};

				Assert.IsFalse(autoDetectIsCalled);
				Assert.AreEqual(languageSelector, new LanguageSelectorFactory().AutoDetect());
				Assert.IsTrue(autoDetectIsCalled);
			}
		}

		[TestMethod]
		public void AutoDetect_WithBooleanParameter_ShouldCallAutoDetectWithBooleanParameterOfTheWrappedLanguageSelector()
		{
			using(ShimsContext.Create())
			{
				bool autoDetectIsCalled = false;
				ILanguageSelector languageSelector = Mock.Of<ILanguageSelector>();
				bool? enableMasterLanguageFallbackValue = null;

				ShimLanguageSelector.AutoDetectBoolean = delegate(bool enableMasterLanguageFallback)
				{
					enableMasterLanguageFallbackValue = enableMasterLanguageFallback;
					autoDetectIsCalled = true;
					return languageSelector;
				};

				bool enableMasterLanguageFallbackParameter = DateTime.Now.Second%2 == 0;

				Assert.IsFalse(autoDetectIsCalled);
				Assert.AreEqual(languageSelector, new LanguageSelectorFactory().AutoDetect(enableMasterLanguageFallbackParameter));
				Assert.IsTrue(autoDetectIsCalled);
				Assert.AreEqual(enableMasterLanguageFallbackValue.Value, enableMasterLanguageFallbackParameter);
			}
		}

		[TestMethod]
		public void Create_ShouldCreateALanguageSelector()
		{
			using(ShimsContext.Create())
			{
				bool constructorIsCalled = false;
				string languageBranchValue = null;
				LanguageSelector languageSelectorValue = null;

				ShimLanguageSelector.ConstructorString = delegate(LanguageSelector languageSelector, string languageBranch)
				{
					constructorIsCalled = true;
					languageSelectorValue = languageSelector;
					languageBranchValue = languageBranch;
				};

				const string languageBranchParameter = "Test";

				Assert.IsFalse(constructorIsCalled);
				new LanguageSelectorFactory().Create(languageBranchParameter);
				Assert.IsTrue(constructorIsCalled);
				Assert.AreEqual(languageBranchParameter, languageBranchValue);
				Assert.IsNotNull(languageSelectorValue);
			}
		}

		[TestMethod]
		public void Fallback_ShouldCallFallbackOfTheWrappedLanguageSelector()
		{
			using(ShimsContext.Create())
			{
				bool fallbackIsCalled = false;
				ILanguageSelector languageSelector = Mock.Of<ILanguageSelector>();
				string preferredLanguageBranchValue = null;
				bool? enableMasterLanguageFallbackValue = null;

				ShimLanguageSelector.FallbackStringBoolean = delegate(string preferredLanguageBranch, bool enableMasterLanguageFallback)
				{
					fallbackIsCalled = true;
					preferredLanguageBranchValue = preferredLanguageBranch;
					enableMasterLanguageFallbackValue = enableMasterLanguageFallback;
					return languageSelector;
				};

				const string preferredLanguageBranchParameter = "Test";
				bool enableMasterLanguageFallbackParameter = DateTime.Now.Second%2 == 0;

				Assert.IsFalse(fallbackIsCalled);
				Assert.AreEqual(languageSelector, new LanguageSelectorFactory().Fallback(preferredLanguageBranchParameter, enableMasterLanguageFallbackParameter));
				Assert.IsTrue(fallbackIsCalled);
				Assert.AreEqual(preferredLanguageBranchParameter, preferredLanguageBranchValue);
				Assert.AreEqual(enableMasterLanguageFallbackValue.Value, enableMasterLanguageFallbackParameter);
			}
		}

		[TestMethod]
		public void MasterLanguage_ShouldCallMasterLanguageOfTheWrappedLanguageSelector()
		{
			using(ShimsContext.Create())
			{
				bool masterLanguageCalled = false;
				ILanguageSelector languageSelector = Mock.Of<ILanguageSelector>();

				ShimLanguageSelector.MasterLanguage = delegate
				{
					masterLanguageCalled = true;
					return languageSelector;
				};

				Assert.IsFalse(masterLanguageCalled);
				Assert.AreEqual(languageSelector, new LanguageSelectorFactory().MasterLanguage());
				Assert.IsTrue(masterLanguageCalled);
			}
		}

		#endregion
	}
}