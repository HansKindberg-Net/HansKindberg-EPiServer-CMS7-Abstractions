using System;

namespace EPiServer.Core
{
	public interface IChangeTrackable
	{
		#region Properties

		DateTime Changed { get; set; }
		string ChangedBy { get; set; }
		DateTime Created { get; set; }
		string CreatedBy { get; set; }
		DateTime? Deleted { get; set; }
		string DeletedBy { get; set; }
		DateTime Saved { get; set; }
		bool SetChangedOnPublish { get; set; }

		#endregion
	}
}