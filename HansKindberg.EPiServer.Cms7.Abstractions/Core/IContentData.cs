using System.Diagnostics.CodeAnalysis;

namespace EPiServer.Core
{
	public interface IContentData
	{
		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Property")]
		PropertyDataCollection Property { get; }

		#endregion
	}
}