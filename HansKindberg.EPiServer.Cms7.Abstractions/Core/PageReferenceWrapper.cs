using System;
using System.Globalization;
using EPiServer.Core;
using HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions;

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

		public override int CompareTo(object obj)
		{
			if(ReferenceEquals(obj, null))
				throw new ArgumentNullException("obj");

			PageReferenceWrapper pageReferenceWrapper = obj as PageReferenceWrapper;

			if(ReferenceEquals(pageReferenceWrapper, null))
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The passed object is not of type \"{0}\".", this.GetType().FullName));

			return ReferenceEquals(this, pageReferenceWrapper) ? 0 : this.PageReference.CompareTo(pageReferenceWrapper.PageReference);
		}

		public override bool CompareToIgnoreWorkID(ContentReference contentReference)
		{
			return this.PageReference.CompareToIgnoreWorkID(contentReference.ToPageReference());
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

		public override bool Equals(object obj)
		{
			return this.PageReference.Equals(obj);
		}

		public static PageReferenceWrapper FromPageReference(PageReference pageReference)
		{
			return pageReference;
		}

		public override int GetHashCode()
		{
			return this.PageReference.GetHashCode();
		}

		public override void MakeReadOnly()
		{
			this.PageReference.MakeReadOnly();
		}

		public override ContentReference ParseReference(string complexReference)
		{
			return new PageReferenceWrapper(PageReference.Parse(complexReference));
		}

		public override string ToString()
		{
			return this.PageReference.ToString();
		}

		#endregion

		#region Implicit operator

		public static implicit operator PageReferenceWrapper(PageReference pageReference)
		{
			return pageReference == null ? null : new PageReferenceWrapper(pageReference);
		}

		public static implicit operator PageReference(PageReferenceWrapper pageReferenceWrapper)
		{
			return pageReferenceWrapper == null ? null : pageReferenceWrapper.PageReference;
		}

		#endregion
	}
}