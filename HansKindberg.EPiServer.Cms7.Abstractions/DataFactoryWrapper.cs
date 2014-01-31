using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Web;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions // ReSharper restore CheckNamespace
{
	public class DataFactoryWrapper : IContentRepository
	{
		#region Fields

		private readonly DataFactory _dataFactory;
		private readonly IPageDataCaster _pageDataCaster;
		private readonly IPermanentLinkMapper _permanentLinkMapper;

		#endregion

		#region Constructors

		public DataFactoryWrapper(DataFactory dataFactory, IPageDataCaster pageDataCaster, IPermanentLinkMapper permanentLinkMapper)
		{
			if(dataFactory == null)
				throw new ArgumentNullException("dataFactory");

			if(pageDataCaster == null)
				throw new ArgumentNullException("pageDataCaster");

			if(permanentLinkMapper == null)
				throw new ArgumentNullException("permanentLinkMapper");

			this._dataFactory = dataFactory;
			this._pageDataCaster = pageDataCaster;
			this._permanentLinkMapper = permanentLinkMapper;
		}

		#endregion

		#region Properties

		protected internal virtual DataFactory DataFactory
		{
			get { return this._dataFactory; }
		}

		protected internal virtual IPageDataCaster PageDataCaster
		{
			get { return this._pageDataCaster; }
		}

		protected internal virtual IPermanentLinkMapper PermanentLinkMapper
		{
			get { return this._permanentLinkMapper; }
		}

		#endregion

		#region Methods

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
				return this.PageDataCaster.CastToContentOrThrowTypeMismatchException<T>(this.GetPage(contentLink.ToPageReference()));
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
				return this.PageDataCaster.CastToContentOrThrowTypeMismatchException<T>(this.GetPage(contentLink.ToPageReference(), selector));
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
			if(contentLink == null)
				throw new ArgumentNullException("contentLink");

			return this.DataFactory.GetDescendents(contentLink.ToPageReference()).Select(pageLink => (ContentReference) pageLink);
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
			this.DataFactory.MoveToWastebasket(contentLink.ToPageReference());
		}

		public virtual ContentReference Save(IContent content, SaveAction action, AccessLevel access)
		{
			if(content == null)
				throw new ArgumentNullException("content");

			// ReSharper disable SuspiciousTypeConversion.Global
			PageData pageData = content as PageData;
			// ReSharper restore SuspiciousTypeConversion.Global

			if(pageData == null)
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "It is only possible to save content inheriting from \"{0}\".", typeof(PageData)));

			return this.DataFactory.Save(pageData, action, access);
		}

		protected internal virtual void ThrowArgumentNullExceptionIfContentLinkIsNullOrEmpty(ContentReference contentLink)
		{
			if(ContentReference.IsNullOrEmpty(contentLink))
				throw new ArgumentNullException("contentLink", "The provided content link does not have a value.");
		}

		#endregion
	}
}