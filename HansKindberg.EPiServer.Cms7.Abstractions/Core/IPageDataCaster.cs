using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core // ReSharper restore CheckNamespace
{
	public interface IPageDataCaster
	{
		#region Methods

		T CastToContentOrThrowTypeMismatchException<T>(PageData pageData) where T : IContentData;

		#endregion
	}
}