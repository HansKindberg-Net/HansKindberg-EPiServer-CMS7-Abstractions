using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Core;
using EPiServer.DataAbstraction;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction // ReSharper restore CheckNamespace
{
	public class PageVersionWrapper : IContentVersionRepository
	{
		#region Methods

		public virtual void Delete(ContentReference contentLink)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<ContentVersion> List(ContentReference contentLink)
		{
			return PageVersion.List(contentLink.ToPageReference()).Select(pageVersion => (ContentVersion) pageVersion).ToArray();
		}

		public virtual IEnumerable<ContentVersion> List(ContentReference contentLink, string languageBranch)
		{
			return PageVersion.List(contentLink.ToPageReference(), languageBranch).Select(pageVersion => (ContentVersion) pageVersion).ToArray();
		}

		public virtual IEnumerable<ContentReference> ListDelayedPublish()
		{
			return PageVersion.ListDelayedPublish().Select(pageData => (ContentReference) pageData.PageLink).ToArray();
		}

		public virtual IEnumerable<ContentVersion> ListPublished(ContentReference contentLink)
		{
			return PageVersion.ListPublishedVersions(contentLink.ToPageReference()).Select(pageVersion => (ContentVersion) pageVersion).ToArray();
		}

		public virtual ContentVersion Load(ContentReference contentLink)
		{
			return PageVersion.Load(contentLink.ToPageReference());
		}

		public virtual ContentVersion LoadCommonDraft(ContentReference contentLink, string language)
		{
			throw new NotImplementedException();
		}

		public virtual ContentVersion LoadPublished(ContentReference contentLink)
		{
			return PageVersion.LoadPublishedVersion(contentLink.ToPageReference());
		}

		public virtual ContentVersion LoadPublished(ContentReference contentLink, string languageBranch)
		{
			return PageVersion.LoadPublishedVersion(contentLink.ToPageReference(), languageBranch);
		}

		public virtual void SetCommonDraft(ContentReference contentLink)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}