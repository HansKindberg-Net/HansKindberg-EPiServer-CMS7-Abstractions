using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using HansKindberg.EPiServer.Cms7.Abstractions.Core.Extensions;

namespace EPiServer.Core
{
	[Serializable]
	[SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
	public class ContentReference : IComparable, IReadOnly
	{
		// ReSharper disable InconsistentNaming

		#region Fields

		private static readonly ContentReference _emptyReference = new ContentReference();
		private readonly PageReference _pageReference;
		private static PageReference _rootPage;
		private static readonly ContentReference _selfReference = new ContentReference(0, -1, null);
		private static PageReference _startPage;
		private static PageReference _wasteBasket;

		#endregion

		#region Constructors

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static ContentReference()
		{
			_emptyReference.MakeReadOnly();
			_selfReference.MakeReadOnly();
		}

		public ContentReference()
		{
			this._pageReference = new PageReference();
		}

		protected internal ContentReference(PageReference pageReference)
		{
			if(pageReference == null)
				throw new ArgumentNullException("pageReference");

			this._pageReference = pageReference;
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID)
		{
			this._pageReference = new PageReference(contentID);
		}

		public ContentReference(string complexReference)
		{
			this._pageReference = new PageReference(complexReference);
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID, bool getPublishedOrLatest)
		{
			this._pageReference = new PageReference(contentID, getPublishedOrLatest);
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID, int versionID)
		{
			this._pageReference = new PageReference(contentID, versionID);
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID, string providerName)
		{
			this._pageReference = new PageReference(contentID, providerName);
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID, int versionID, string providerName)
		{
			this._pageReference = new PageReference(contentID, versionID, providerName);
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public ContentReference(int contentID, int versionID, string providerName, bool getPublishedOrLatest)
		{
			this._pageReference = new PageReference(contentID, versionID, providerName, getPublishedOrLatest);
		}

		#endregion

		#region Properties

		public static ContentReference EmptyReference
		{
			get { return _emptyReference; }
		}

		public bool GetPublishedOrLatest
		{
			get { return this._pageReference.IsAnyVersion(); }
		}

		public static ContentReference GlobalBlockFolder { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public int ID
		{
			get { return this._pageReference.ID; }
			set { this._pageReference.ID = value; }
		}

		public bool IsExternalProvider
		{
			get { return !string.IsNullOrEmpty(this.ProviderName); }
		}

		[XmlIgnore]
		public bool IsReadOnly
		{
			get { return this._pageReference.IsReadOnly; }
		}

		protected internal PageReference PageReference
		{
			get { return this._pageReference; }
		}

		public string ProviderName
		{
			get { return this._pageReference.RemoteSite; }
			set { this._pageReference.RemoteSite = value; }
		}

		public static PageReference RootPage
		{
			get { return _rootPage ?? (_rootPage = PageReference.RootPage); }
			set { _rootPage = value; }
		}

		public static ContentReference SelfReference
		{
			get { return _selfReference; }
		}

		public static ContentReference SiteBlockFolder { get; set; }

		public static PageReference StartPage
		{
			get { return _startPage ?? (_startPage = PageReference.StartPage); }
			set { _startPage = value; }
		}

		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WasteBasket")]
		public static PageReference WasteBasket
		{
			get { return _wasteBasket ?? (_wasteBasket = PageReference.WasteBasket); }
			set { _wasteBasket = value; }
		}

		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public int WorkID
		{
			get { return this._pageReference.WorkID; }
			set { this._pageReference.WorkID = value; }
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#")]
		public virtual int CompareTo(object x)
		{
			ContentReference contentReference = x as ContentReference;

			if(contentReference == null)
				throw new ArgumentException("Object is not a ContentReference");

			if(this == contentReference)
				return 0;

			if(this.ID > contentReference.ID)
				return 1;

			return -1;
		}

		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public virtual bool CompareToIgnoreWorkID(ContentReference contentReference)
		{
			return this._pageReference.CompareToIgnoreWorkID(contentReference._pageReference);
		}

		public ContentReference Copy()
		{
			return (ContentReference) this.MemberwiseClone();
		}

		public ContentReference CreateReferenceWithoutVersion()
		{
			return new ContentReference(this._pageReference.CreateReferenceToPublishedPage());
		}

		public virtual object CreateWritableClone()
		{
			return new ContentReference(this._pageReference.CreateWritableClone());
		}

		[SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#")]
		public override bool Equals(object o)
		{
			ContentReference contentReference = o as ContentReference;

			return contentReference != null && this._pageReference.Equals(contentReference._pageReference);
		}

		public override int GetHashCode()
		{
			return this._pageReference.GetHashCode();
		}

		public static bool IsNullOrEmpty(ContentReference contentLink)
		{
			return contentLink == null || PageReference.IsNullOrEmpty(contentLink._pageReference);
		}

		public virtual void MakeReadOnly()
		{
			this._pageReference.MakeReadOnly();
		}

		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ContentReference")]
		public static ContentReference Parse(string s)
		{
			ContentReference contentReference;

			if(!TryParse(s, out contentReference))
				throw new EPiServerException("ContentReference: Input string was not in a correct format.");

			return contentReference;
		}

		public virtual ContentReference ParseReference(string complexReference)
		{
			ContentReference contentReference;

			return TryParse(complexReference, out contentReference) ? contentReference : null;
		}

		public override string ToString()
		{
			return this._pageReference.ToString();
		}

		public static bool TryParse(string complexReference, out ContentReference result)
		{
			PageReference pageReference;

			bool tryParse = PageReference.TryParse(complexReference, out pageReference);

			result = pageReference.ToContentReference();

			return tryParse;
		}

		#endregion

		#region Operators

		public static bool operator ==(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			if(ReferenceEquals(firstContentReference, secondContentReference))
				return true;

			if(ReferenceEquals(firstContentReference, null) || ReferenceEquals(secondContentReference, null))
				return false;

			return firstContentReference.Equals(secondContentReference);
		}

		public static bool operator !=(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			return !(firstContentReference == secondContentReference);
		}

		#endregion

		// ReSharper restore InconsistentNaming
	}
}