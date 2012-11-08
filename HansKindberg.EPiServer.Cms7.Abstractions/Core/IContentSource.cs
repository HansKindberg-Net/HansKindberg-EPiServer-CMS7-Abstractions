using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EPiServer.Core
{
	public interface IContentSource
	{
		#region Properties

		IContent CurrentContent { get; }

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		T Get<T>(ContentReference contentLink) where T : IContentData;

		IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData;

		#endregion
	}
}