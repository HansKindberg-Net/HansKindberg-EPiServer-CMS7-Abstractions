namespace EPiServer.Core
{
	public class LanguageSelectorFactory
	{
		#region Methods

		public virtual ILanguageSelector AutoDetect()
		{
			return LanguageSelector.AutoDetect();
		}

		public virtual ILanguageSelector AutoDetect(bool enableMasterLanguageFallback)
		{
			return LanguageSelector.AutoDetect(enableMasterLanguageFallback);
		}

		public virtual ILanguageSelector Create(string languageBranch)
		{
			return new LanguageSelector(languageBranch);
		}

		public virtual ILanguageSelector Fallback(string preferredLanguageBranch, bool enableMasterLanguageFallback)
		{
			return LanguageSelector.Fallback(preferredLanguageBranch, enableMasterLanguageFallback);
		}

		public virtual ILanguageSelector MasterLanguage()
		{
			return LanguageSelector.MasterLanguage();
		}

		#endregion
	}
}