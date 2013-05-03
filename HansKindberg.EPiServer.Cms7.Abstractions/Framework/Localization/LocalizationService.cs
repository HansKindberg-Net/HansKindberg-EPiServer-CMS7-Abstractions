using System.Globalization;

namespace EPiServer.Framework.Localization
{
	public abstract class LocalizationService
	{
		#region Methods

		public string GetString(string resourceKey)
		{
			return this.GetStringByCulture(resourceKey, CultureInfo.CurrentUICulture);
		}

		public string GetString(string resourceKey, string fallback)
		{
			return this.GetStringByCulture(resourceKey, fallback, CultureInfo.CurrentUICulture);
		}

		public abstract string GetStringByCulture(string resourceKey, CultureInfo culture);
		public abstract string GetStringByCulture(string resourceKey, string fallback, CultureInfo culture);

		#endregion
	}
}