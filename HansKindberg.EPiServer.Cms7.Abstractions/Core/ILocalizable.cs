using System.Collections.Generic;
using System.Globalization;

namespace EPiServer.Core
{
	public interface ILocalizable : ILocale
	{
		#region Properties

		IEnumerable<CultureInfo> ExistingLanguages { get; set; }
		CultureInfo MasterLanguage { get; set; }

		#endregion
	}
}