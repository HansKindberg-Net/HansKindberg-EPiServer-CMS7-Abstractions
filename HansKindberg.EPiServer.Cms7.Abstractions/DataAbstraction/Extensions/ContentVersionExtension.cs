using EPiServer.Core;
using EPiServer.DataAbstraction;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.DataAbstraction.Extensions // ReSharper restore CheckNamespace
{
	public static class ContentVersionExtension
	{
		#region Methods

		public static PageVersion ToPageVersion(this ContentVersion contentVersion)
		{
			return contentVersion == null ? null : new PageVersion(contentVersion.ContentLink.ToPageReference(), contentVersion.Name, contentVersion.Status, contentVersion.Saved, contentVersion.SavedBy, contentVersion.StatusChangedBy, contentVersion.MasterVersionID, contentVersion.LanguageBranch, contentVersion.IsMasterLanguageBranch);
		}

		#endregion
	}
}