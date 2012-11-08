using System.Collections.Generic;

namespace EPiServer.Core
{
	public interface IContentSource
	{
		#region Properties

		IContent CurrentContent { get; }

		#endregion

		#region Methods

		T Get<T>(ContentReference contentLink) where T : IContentData;
		IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData;

		#endregion
	}
}