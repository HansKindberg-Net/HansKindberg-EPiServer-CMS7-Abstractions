using System.Diagnostics.CodeAnalysis;

namespace EPiServer.Core
{
	public interface IResourceable
	{
		// ReSharper disable InconsistentNaming

		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		long ContentFolderID { get; set; }

		#endregion

		// ReSharper restore InconsistentNaming
	}
}