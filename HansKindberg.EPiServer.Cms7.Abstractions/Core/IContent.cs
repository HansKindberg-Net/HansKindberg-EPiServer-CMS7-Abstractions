using System;
using System.Diagnostics.CodeAnalysis;

namespace EPiServer.Core
{
	public interface IContent : IContentData
	{
		#region Properties

		Guid ContentGuid { get; set; }
		ContentReference ContentLink { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		int ContentTypeID { get; set; }

		// ReSharper restore InconsistentNaming
		bool IsDeleted { get; set; }
		string Name { get; set; }
		//ContentReference ParentLink { get; set; } // Can not implement this yet.

		#endregion
	}
}