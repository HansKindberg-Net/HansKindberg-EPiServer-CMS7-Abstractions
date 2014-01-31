using System;

namespace EPiServer.Core
{
	public interface IVersionable
	{
		#region Properties

		bool IsPendingPublish { get; set; }
		DateTime? StartPublish { get; set; }
		VersionStatus Status { get; set; }
		DateTime? StopPublish { get; set; }

		#endregion
	}
}