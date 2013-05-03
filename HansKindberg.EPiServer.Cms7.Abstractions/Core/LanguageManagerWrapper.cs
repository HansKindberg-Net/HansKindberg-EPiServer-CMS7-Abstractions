using System;
using System.Globalization;
using EPiServer.Framework.Localization;

namespace EPiServer.Core
{
	public class LanguageManagerWrapper : LocalizationService
	{
		#region Fields

		private readonly LanguageManager _languageManager;

		#endregion

		#region Constructors

		public LanguageManagerWrapper(LanguageManager languageManager)
		{
			if(languageManager == null)
				throw new ArgumentNullException("languageManager");

			this._languageManager = languageManager;
		}

		#endregion

		#region Properties

		protected internal LanguageManager LanguageManager
		{
			get { return this._languageManager; }
		}

		#endregion

		#region Methods

		public override string GetStringByCulture(string resourceKey, CultureInfo culture)
		{
			if(culture == null)
				throw new ArgumentNullException("culture");

			return this._languageManager.Translate(resourceKey, culture.Name);
		}

		public override string GetStringByCulture(string resourceKey, string fallback, CultureInfo culture)
		{
			if(culture == null)
				throw new ArgumentNullException("culture");

			return this._languageManager.TranslateFallback(resourceKey, fallback, culture.Name);
		}

		#endregion
	}
}