using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions // ReSharper restore CheckNamespace
{
	public class DataFactoryWrapper : IContentRepository
	{
		#region Fields

		private readonly DataFactory _dataFactory;

		#endregion

		#region Constructors

		public DataFactoryWrapper(DataFactory dataFactory)
		{
			if(dataFactory == null)
				throw new ArgumentNullException("dataFactory");

			this._dataFactory = dataFactory;
		}

		#endregion

		#region Properties

		protected internal DataFactory DataFactory
		{
			get { return this._dataFactory; }
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
			throw new NotImplementedException();
		}

		public virtual T Get<T>(Guid contentGuid) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T Get<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T Get<T>(Guid contentGuid, ILanguageSelector selector) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<IContent> GetAncestors(ContentReference contentLink)
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual IEnumerable<T> GetChildren<T>(ContentReference contentLink, ILanguageSelector selector, int startIndex, int maxRows) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T GetDefault<T>(ContentReference parentLink) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T GetDefault<T>(ContentReference parentLink, ILanguageSelector languageSelector) where T : IContentData
		{
			throw new NotImplementedException();
		}

		public virtual T GetDefault<T>(ContentReference parentLink, int contentTypeID, ILanguageSelector languageSelector) where T : IContentData
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

		#endregion
	}
}