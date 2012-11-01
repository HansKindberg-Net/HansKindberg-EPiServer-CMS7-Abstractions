namespace EPiServer.Core
{
	public interface IModifiedTrackable
	{
		#region Properties

		bool IsModified { get; }

		#endregion

		#region Methods

		void ResetModified();

		#endregion
	}
}