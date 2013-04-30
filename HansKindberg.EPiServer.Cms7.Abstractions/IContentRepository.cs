using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EPiServer
{
	public interface IContentRepository : IContentLoader
	{
		#region Methods

		ContentReference Copy(ContentReference source, ContentReference destination, AccessLevel requiredSourceAccess, AccessLevel requiredDestinationAccess, bool publishOnDestination);
		T CreateLanguageBranch<T>(ContentReference contentLink, ILanguageSelector languageSelector, AccessLevel access) where T : IContentData;
		void Delete(ContentReference contentLink, bool forceDelete, AccessLevel access);
		void DeleteChildren(ContentReference contentLink, bool forceDelete, AccessLevel access);
		void DeleteLanguageBranch(ContentReference contentLink, string languageBranch, AccessLevel access);
		T GetDefault<T>(ContentReference parentLink) where T : IContentData;
		T GetDefault<T>(ContentReference parentLink, ILanguageSelector languageSelector) where T : IContentData;
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		T GetDefault<T>(ContentReference parentLink, int contentTypeID, ILanguageSelector languageSelector) where T : IContentData;

		// ReSharper restore InconsistentNaming
		IEnumerable<T> GetLanguageBranches<T>(ContentReference contentLink) where T : IContentData;
		//IEnumerable<ReferenceInformation> GetReferencesToContent(ContentReference contentLink, bool includeDecendents);
		IEnumerable<IContent> ListDelayedPublish();
		void Move(ContentReference contentLink, ContentReference destination, AccessLevel requiredSourceAccess, AccessLevel requiredDestinationAccess);
		void MoveToWastebasket(ContentReference contentLink, string deletedBy);
		ContentReference Save(IContent content, SaveAction action, AccessLevel access);

		#endregion
	}
}