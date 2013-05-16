using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Web;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions // ReSharper restore CheckNamespace
{
	public class DataFactoryWrapper : IContentRepository
	{
		#region Fields

		private readonly DataFactory _dataFactory;
		private readonly IPermanentLinkMapper _permanentLinkMapper;

		#endregion

		#region Constructors

		public DataFactoryWrapper(DataFactory dataFactory, IPermanentLinkMapper permanentLinkMapper)
		{
			if(dataFactory == null)
				throw new ArgumentNullException("dataFactory");

			if(permanentLinkMapper == null)
				throw new ArgumentNullException("permanentLinkMapper");

			this._dataFactory = dataFactory;
			this._permanentLinkMapper = permanentLinkMapper;
		}

		#endregion

		#region Properties

		protected internal DataFactory DataFactory
		{
			get { return this._dataFactory; }
		}

		protected internal IPermanentLinkMapper PermanentLinkMapper
		{
			get { return this._permanentLinkMapper; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "CastOr")]
		protected internal virtual T CastOrThrowTypeMismatchException<T>(PageData pageData, ContentReference contentLink) where T : IContentData
		{
			if(pageData != null)
			{
				Type actualType = pageData.GetType();
				Type requiredType = typeof(T);

				if(!requiredType.IsAssignableFrom(actualType))
					this.ThrowTypeMismatchException(contentLink, actualType, requiredType);
			}

			return (T) (object) pageData;
		}

		public virtual ContentReference Copy(ContentReference source, ContentReference destination, AccessLevel requiredSourceAccess, AccessLevel requiredDestinationAccess, bool publishOnDestination)
		{
			throw new NotImplementedException();
		}

		public virtual T CreateLanguageBranch<T>(ContentReference contentLink, ILanguageSelector languageSelector, AccessLevel access) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual void Delete(ContentReference contentLink, bool forceDelete, AccessLevel access)
		{
			throw new NotImplementedException();
		}

		public virtual void DeleteChildren(ContentReference contentLink, bool forceDelete, AccessLevel access)
		{
			throw new NotImplementedException();
		}

		public virtual void DeleteLanguageBranch(ContentReference contentLink, string languageBranch, AccessLevel access)
		{
			throw new NotImplementedException();
		}

		public virtual T Get<T>(ContentReference contentLink) where T : IContentData
		{
			this.ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(contentLink);

			try
			{
				return this.CastOrThrowTypeMismatchException<T>(this.GetPage(contentLink.ToPageReference()), contentLink);
			}
			catch(PageNotFoundException)
			{
				throw new ContentNotFoundException(contentLink);
			}
		}

		public virtual T Get<T>(Guid contentGuid) where T : IContentData
		{
			PermanentPageLinkMap permanentPageLinkMap = this.PermanentLinkMapper.Find(contentGuid) as PermanentPageLinkMap;

			if(permanentPageLinkMap == null)
				throw new ContentNotFoundException(contentGuid);

			return this.Get<T>(permanentPageLinkMap.PageReference);
		}

		public virtual T Get<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData
		{
			this.ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(contentLink);

			try
			{
				return this.CastOrThrowTypeMismatchException<T>(this.GetPage(contentLink.ToPageReference(), selector), contentLink);
			}
			catch(PageNotFoundException)
			{
				throw new ContentNotFoundException(contentLink);
			}
		}

		public virtual T Get<T>(Guid contentGuid, ILanguageSelector selector) where T : IContentData
		{
			PermanentPageLinkMap permanentPageLinkMap = this.PermanentLinkMapper.Find(contentGuid) as PermanentPageLinkMap;

			if(permanentPageLinkMap == null)
				throw new ContentNotFoundException(contentGuid);

			return this.Get<T>(permanentPageLinkMap.PageReference, selector);
		}

		public virtual IEnumerable<IContent> GetAncestors(ContentReference contentLink)
		{
			throw new NotImplementedException();
		}

		public virtual PageDataCollection GetChildren(PageReference pageLink)
		{
			return this.DataFactory.GetChildren(pageLink);
		}

		public virtual PageDataCollection GetChildren(PageReference pageLink, ILanguageSelector selector)
		{
			return this.DataFactory.GetChildren(pageLink, selector);
		}

		public virtual PageDataCollection GetChildren(PageReference pageLink, ILanguageSelector selector, int startIndex, int maxRows)
		{
			return this.DataFactory.GetChildren(pageLink, selector, startIndex, maxRows);
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData
		{
			this.ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(contentLink);

			try
			{
				return this.GetChildren(contentLink.ToPageReference()).OfType<T>();
			}
			catch(PageNotFoundException)
			{
				throw new ContentNotFoundException(contentLink);
			}
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData
		{
			this.ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(contentLink);

			try
			{
				return this.GetChildren(contentLink.ToPageReference(), selector).OfType<T>();
			}
			catch(PageNotFoundException)
			{
				throw new ContentNotFoundException(contentLink);
			}
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector, int startIndex, int maxRows) where T : IContentData
		{
			this.ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(contentLink);

			try
			{
				return this.GetChildren(contentLink.ToPageReference(), selector, startIndex, maxRows).OfType<T>();
			}
			catch(PageNotFoundException)
			{
				throw new ContentNotFoundException(contentLink);
			}
		}

		public virtual T GetDefault<T>(ContentReference parentLink) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T GetDefault<T>(ContentReference parentLink, ILanguageSelector languageSelector) where T : IContentData
		{
			throw new NotImplementedException();
		}

		// ReSharper disable InconsistentNaming
		public virtual T GetDefault<T>(ContentReference parentLink, int contentTypeID, ILanguageSelector languageSelector) where T : IContentData // ReSharper restore InconsistentNaming
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<ContentReference> GetDescendents(ContentReference contentLink)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<IContent> GetItems(IEnumerable<ContentReference> contentLinks, ILanguageSelector selector)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<T> GetLanguageBranches<T>(ContentReference contentLink) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual PageData GetPage(PageReference pageLink)
		{
			return this.DataFactory.GetPage(pageLink);
		}

		public virtual PageData GetPage(PageReference pageLink, ILanguageSelector selector)
		{
			return this.DataFactory.GetPage(pageLink, selector);
		}

		public virtual IEnumerable<IContent> ListDelayedPublish()
		{
			throw new NotImplementedException();
		}

		public virtual void Move(ContentReference contentLink, ContentReference destination, AccessLevel requiredSourceAccess, AccessLevel requiredDestinationAccess)
		{
			throw new NotImplementedException();
		}

		public virtual void MoveToWastebasket(ContentReference contentLink, string deletedBy)
		{
			throw new NotImplementedException();
		}

		public virtual ContentReference Save(IContent content, SaveAction action, AccessLevel access)
		{
			throw new NotImplementedException();
		}

		protected internal virtual void ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(ContentReference contentLink)
		{
			if(ContentReference.IsNullOrEmpty(contentLink))
				throw new ArgumentNullException("contentLink", "The provided content link does not have a value.");
		}

		protected internal virtual void ThrowTypeMismatchException(ContentReference contentLink, Type actualType, Type requiredType)
		{
			throw new TypeMismatchException(string.Format(CultureInfo.InvariantCulture, "Content with id '{0}' is of type '{1}' which does not inherit required type '{2}'", contentLink, actualType, requiredType));
		}

		#endregion
	}
}