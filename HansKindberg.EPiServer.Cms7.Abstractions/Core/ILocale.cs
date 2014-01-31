using System.Globalization;

namespace EPiServer.Core
{
	public interface ILocale
	{
		#region Properties

		CultureInfo Language { get; set; }

		#endregion
	}
}