using System;
using System.Globalization;
using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core // ReSharper restore CheckNamespace
{
	public class PageDataCaster : IPageDataCaster
	{
		#region Methods

		public virtual T CastToContentOrThrowTypeMismatchException<T>(PageData pageData) where T : IContentData
		{
			if(pageData != null)
			{
				Type actualType = pageData.GetType();
				Type requiredType = typeof(T);

				if(!requiredType.IsAssignableFrom(actualType))
					this.ThrowTypeMismatchException(pageData.PageLink, actualType, requiredType);
			}

			return (T) (object) pageData;
		}

		protected internal virtual void ThrowTypeMismatchException(PageReference pageLink, Type actualType, Type requiredType)
		{
			throw new TypeMismatchException(string.Format(CultureInfo.InvariantCulture, "Content with id \"{0}\" is of type \"{1}\" which does not inherit required type \"{2}\".", pageLink, actualType, requiredType));
		}

		#endregion
	}
}