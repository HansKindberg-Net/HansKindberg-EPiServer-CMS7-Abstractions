namespace EPiServer.Core
{
	public interface IContentData
	{
		#region Properties

		bool IsNull { get; }
		PropertyDataCollection Properties { get; }

		#endregion
	}
}