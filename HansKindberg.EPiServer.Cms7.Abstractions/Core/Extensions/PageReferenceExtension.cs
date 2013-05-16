using System;
using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions // ReSharper restore CheckNamespace
{
	[Obsolete("Use the implicit operator of ContentReference instead: (ContentReference)pageReference", false)]
	public static class PageReferenceExtension
	{
		#region Methods

		[Obsolete("Use the implicit operator of ContentReference instead: (ContentReference)pageReference", false)]
		public static ContentReference ToContentReference(this PageReference pageLink)
		{
			return pageLink;
		}

		#endregion
	}
}