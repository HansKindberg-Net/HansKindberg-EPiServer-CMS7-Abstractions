namespace EPiServer.Core
{
	public static class ContentReferenceExtensions
	{
		#region Methods

		public static PageReference ToPageReference(this ContentReference contentLink)
		{
			return contentLink == null ? null : contentLink.PageReference;
		}

		#endregion
	}
}