namespace EPiServer.Data.Entity
{
	public interface IReadOnly
	{
		#region Properties

		bool IsReadOnly { get; }

		#endregion

		#region Methods

		object CreateWritableClone();
		void MakeReadOnly();

		#endregion
	}

	public interface IReadOnly<out T> : IReadOnly
	{
		#region Methods

		new T CreateWritableClone();

		#endregion
	}
}