using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions // ReSharper restore CheckNamespace
{
	public static class ContentReferenceExtension
	{
		#region Methods

		public static PageReference ToPageReference(this ContentReference contentReference)
		{
			if(contentReference == null)
				return null;

			PageReferenceWrapper pageReferenceWrapper = contentReference as PageReferenceWrapper;

			if(pageReferenceWrapper != null)
				return pageReferenceWrapper.PageReference;

			return new PageReference(contentReference.ID, contentReference.WorkID, contentReference.ProviderName, contentReference.GetPublishedOrLatest);
		}

		#endregion
	}
}