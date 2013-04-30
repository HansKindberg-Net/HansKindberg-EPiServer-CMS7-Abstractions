using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;

namespace EPiServer
{
	public interface IContentLoader
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		T Get<T>(ContentReference contentLink) where T : IContentData;

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "guid")]
		T Get<T>(Guid contentGuid) where T : IContentData;

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		T Get<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData;

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "guid")]
		T Get<T>(Guid contentGuid, ILanguageSelector selector) where T : IContentData;

		IEnumerable<IContent> GetAncestors(ContentReference contentLink);
		IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData;
		IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData;
		IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector, int startIndex, int maxRows) where T : IContentData;

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Descendents")]
		IEnumerable<ContentReference> GetDescendents(ContentReference contentLink);

		IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, ILanguageSelector selector);

		#endregion
	}
}