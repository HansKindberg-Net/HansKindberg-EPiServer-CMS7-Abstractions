using System.Collections.Generic;
using System.Globalization;

namespace EPiServer.Core
{
	public interface ILocalizable
	{
		#region Properties

		IEnumerable<CultureInfo> ExistingLanguages { get; set; }
		CultureInfo Language { get; set; }
		CultureInfo MasterLanguage { get; set; }

		#endregion
	}
}