using System;
using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Core // ReSharper restore CheckNamespace
{
	public class PageReferenceWrapper : ContentReference
	{
		#region Fields

		private readonly PageReference _pageReference;

		#endregion

		#region Constructors

		public PageReferenceWrapper(PageReference pageReference)
		{
			if(pageReference == null)
				throw new ArgumentNullException("pageReference");

			this._pageReference = pageReference;
		}

		#endregion

		#region Properties

		public override bool GetPublishedOrLatest
		{
			get { return this.PageReference.IsAnyVersion(); }
		}

		public override int ID
		{
			get { return this.PageReference.ID; }
			set { this.PageReference.ID = value; }
		}

		public override bool IsExternalProvider
		{
			get { return this.PageReference.IsRemote(); }
		}

		public override bool IsReadOnly
		{
			get { return this.PageReference.IsReadOnly; }
		}

		protected internal PageReference PageReference
		{
			get { return this._pageReference; }
		}

		public override string ProviderName
		{
			get { return this.PageReference.RemoteSite; }
			set { this.PageReference.RemoteSite = value; }
		}

		public override int WorkID
		{
			get { return this.PageReference.WorkID; }
			set { this.PageReference.WorkID = value; }
		}

		#endregion

		#region Methods

		public override bool CompareToIgnoreWorkID(ContentReference contentReference)
		{
			return this.PageReference.CompareToIgnoreWorkID(ToPageReference(contentReference));
		}

		public override ContentReference Copy()
		{
			return new PageReferenceWrapper(this.PageReference.Copy());
		}

		public override ContentReference CreateReferenceWithoutVersion()
		{
			return new PageReferenceWrapper(this.PageReference.CreateReferenceToPublishedPage());
		}

		public override object CreateWritableClone()
		{
			return new PageReferenceWrapper(this.PageReference.CreateWritableClone());
		}

		public static PageReferenceWrapper FromPageReference(PageReference pageReference)
		{
			return pageReference;
		}

		public override void MakeReadOnly()
		{
			this.PageReference.MakeReadOnly();
		}

		public override ContentReference ParseReference(string complexReference)
		{
			return new PageReferenceWrapper(PageReference.Parse(complexReference));
		}

		protected internal static PageReference ToPageReference(ContentReference contentReference)
		{
			return contentReference == null ? null : new PageReference(contentReference.ID, contentReference.WorkID, contentReference.ProviderName, contentReference.GetPublishedOrLatest);
		}

		#endregion

		#region Implicit operator

		public static implicit operator PageReferenceWrapper(PageReference pageReference)
		{
			return pageReference == null ? null : new PageReferenceWrapper(pageReference);
		}

		#endregion
	}
}