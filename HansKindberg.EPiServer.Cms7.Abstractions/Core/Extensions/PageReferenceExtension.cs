using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions // ReSharper restore CheckNamespace
{
	public static class PageReferenceExtension
	{
		#region Methods

		public static ContentReference ToContentReference(this PageReference pageReference)
		{
			return (PageReferenceWrapper) pageReference;
		}

		#endregion
	}
}