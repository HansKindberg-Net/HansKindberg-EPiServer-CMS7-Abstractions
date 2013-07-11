using System.Collections.Generic;
using EPiServer.DataAbstraction;

namespace EPiServer.Core
{
	public interface IContentVersionRepository
	{
		#region Methods

		void Delete(ContentReference contentLink);
		IEnumerable<ContentVersion> List(ContentReference contentLink);
		IEnumerable<ContentVersion> List(ContentReference contentLink, string languageBranch);
		IEnumerable<ContentReference> ListDelayedPublish();
		IEnumerable<ContentVersion> ListPublished(ContentReference contentLink);
		ContentVersion Load(ContentReference contentLink);
		ContentVersion LoadCommonDraft(ContentReference contentLink, string language);
		ContentVersion LoadPublished(ContentReference contentLink);
		ContentVersion LoadPublished(ContentReference contentLink, string languageBranch);
		void SetCommonDraft(ContentReference contentLink);

		#endregion
	}
}