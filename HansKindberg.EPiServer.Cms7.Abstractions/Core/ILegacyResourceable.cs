using System.Diagnostics.CodeAnalysis;

namespace EPiServer.Core
{
	public interface ILegacyResourceable
	{
		// ReSharper disable InconsistentNaming

		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		long ContentFolderID { get; set; }

		#endregion

		// ReSharper restore InconsistentNaming
	}
}