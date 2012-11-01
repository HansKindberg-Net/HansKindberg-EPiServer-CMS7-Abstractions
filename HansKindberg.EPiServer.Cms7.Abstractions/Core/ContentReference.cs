using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Serialization;

namespace EPiServer.Core
{
	[SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
	[Serializable, TypeConverter(typeof(ContentReferenceConverter<ContentReference>))]
	public class ContentReference : IComparable, IReadOnly
	{
		#region Fields

		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")] public static readonly ContentReference EmptyReference = new ContentReference {IsReadOnly = true};
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")] public static readonly ContentReference SelfReference = new ContentReference(0, -1, null) {IsReadOnly = true};
		private int _contentId;
		private readonly bool _getPublishedOrLatest;
		private bool _isReadOnly;
		private string _providerName;
		private int _versionId;

		#endregion

		#region Constructors

		public ContentReference() {}

		public ContentReference(int contentId)
		{
			this._contentId = contentId;
		}

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ContentReference(string complexReference)
		{
			if(string.IsNullOrEmpty(complexReference))
				throw new EPiServerException("Content-reference string cannot be null/empty");

			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			ContentReference reference = this.ParseReference(complexReference);
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
			if(reference == null)
				throw new EPiServerException("Content-reference: Input string was not in a correct format.");

			this._contentId = reference._contentId;
			this._versionId = reference._versionId;
			this._providerName = reference._providerName;
		}

		public ContentReference(int contentId, bool getPublishedOrLatest) : this(contentId)
		{
			this._getPublishedOrLatest = getPublishedOrLatest;
		}

		public ContentReference(int contentId, int versionId) : this(contentId)
		{
			this._versionId = versionId;
		}

		public ContentReference(int contentId, string providerName) : this(contentId)
		{
			this.ProviderName = providerName;
		}

		public ContentReference(int contentId, int versionId, string providerName) : this(contentId, versionId)
		{
			this.ProviderName = providerName;
		}

		public ContentReference(int contentId, int versionId, string providerName, bool getPublishedOrLatest) : this(contentId, versionId, providerName)
		{
			this._getPublishedOrLatest = getPublishedOrLatest;
		}

		#endregion

		#region Properties

		public bool GetPublishedOrLatest
		{
			get { return this._getPublishedOrLatest; }
		}

		public static ContentReference GlobalBlockFolder { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public int ID // ReSharper restore InconsistentNaming
		{
			get { return this._contentId; }
			set
			{
				this.ThrowIfReadOnly();
				this._contentId = value;
			}
		}

		public bool IsExternalProvider
		{
			get { return !string.IsNullOrEmpty(this.ProviderName); }
		}

		[XmlIgnore]
		public bool IsReadOnly
		{
			get { return this._isReadOnly; }
			protected set { this._isReadOnly = value; }
		}

		public string ProviderName
		{
			get { return this._providerName; }
			set
			{
				this.ThrowIfReadOnly();
				this._providerName = string.IsNullOrEmpty(value) ? null : value;
			}
		}

		public static ContentReference SiteBlockFolder { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public int WorkID // ReSharper restore InconsistentNaming
		{
			get { return this._versionId; }
			set
			{
				this.ThrowIfReadOnly();
				this._versionId = value;
			}
		}

		#endregion

		#region Methods

		public virtual int CompareTo(object obj)
		{
			ContentReference contentReference = obj as ContentReference;

			if(contentReference == null)
				throw new ArgumentException("Object is not a ContentReference");

			if(this == contentReference)
				return 0;

			if(this.ID > contentReference.ID)
				return 1;

			return -1;
		}

		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public virtual bool CompareToIgnoreWorkID(ContentReference contentReference) // ReSharper restore InconsistentNaming
		{
			if(contentReference == null)
				throw new ArgumentNullException("contentReference");

			return this._contentId == contentReference.ID && this._providerName == contentReference.ProviderName;
		}

		public ContentReference Copy()
		{
			return (ContentReference) this.MemberwiseClone();
		}

		public ContentReference CreateReferenceWithoutVersion()
		{
			ContentReference reference = this.CreateWritableClone() as ContentReference;
			// ReSharper disable PossibleNullReferenceException
			reference.WorkID = 0;
			// ReSharper restore PossibleNullReferenceException
			return reference;
		}

		public virtual object CreateWritableClone()
		{
			ContentReference reference = base.MemberwiseClone() as ContentReference;
			// ReSharper disable PossibleNullReferenceException
			reference.IsReadOnly = false;
			// ReSharper restore PossibleNullReferenceException
			return reference;
		}

		public override bool Equals(object obj)
		{
			ContentReference reference = obj as ContentReference;

			if(reference == null)
				return false;

			return this._contentId == reference.ID && this._versionId == reference.WorkID && this._providerName == reference.ProviderName;
		}

		public override int GetHashCode()
		{
			// ReSharper disable NonReadonlyFieldInGetHashCode
			return this._contentId + this._versionId + (this._providerName == null ? 0 : this._providerName.GetHashCode());
			// ReSharper restore NonReadonlyFieldInGetHashCode
		}

		public static bool IsNullOrEmpty(ContentReference contentLink)
		{
			return contentLink == null || (contentLink.ID == 0 && contentLink.WorkID == 0 && contentLink.ProviderName == null);
		}

		public virtual void MakeReadOnly()
		{
			this._isReadOnly = true;
		}

		public static ContentReference Parse(string value)
		{
			ContentReference reference;

			if(!TryParse(value, out reference))
				throw new EPiServerException("Content-reference: Input string was not in a correct format.");

			return reference;
		}

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		public virtual ContentReference ParseReference(string complexReference)
		{
			ContentReference reference;
			return TryParse(complexReference, out reference) ? reference : null;
		}

		protected void ThrowIfReadOnly()
		{
			HansKindberg.EPiServer.Cms7.Abstractions.Data.Validator.ValidateNotReadOnly(this);
		}

		public override string ToString()
		{
			if(this._contentId == 0)
				return this._versionId == -1 ? "-" : string.Empty;

			string contentReferenceString = this._contentId.ToString(CultureInfo.InvariantCulture);

			if(this._versionId != 0)
				contentReferenceString = contentReferenceString + "_" + this._versionId.ToString(CultureInfo.InvariantCulture);

			if(this._providerName == null)
				return contentReferenceString;

			if(this._versionId == 0)
				return contentReferenceString + "__" + this._providerName;

			return contentReferenceString + "_" + this._providerName;
		}

		public static bool TryParse(string complexReference, out ContentReference result)
		{
			int id;
			result = EmptyReference;

			if(string.IsNullOrEmpty(complexReference))
				return complexReference != null;

			if(complexReference == "-")
			{
				result = SelfReference;
				return true;
			}

			string[] stringArray = complexReference.Split(new[] {'_'});
			if(stringArray.Length > 3)
				return false;

			if(!int.TryParse(stringArray[0], out id))
				return false;

			if(stringArray.Length == 1)
			{
				result = new ContentReference(id);
				return true;
			}

			int workId = 0;
			if(stringArray[1].Length > 0 && !int.TryParse(stringArray[1], out workId))
				return false;

			if(stringArray.Length == 3)
			{
				result = new ContentReference(id, workId, stringArray[2]);
				return true;
			}

			result = new ContentReference(id, workId);
			return true;
		}

		#endregion

		#region Other members

		public static bool operator ==(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			if(firstContentReference == null)
				return secondContentReference == null;

			return firstContentReference.Equals(secondContentReference);
		}

		public static bool operator !=(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			if(firstContentReference == null)
				return secondContentReference != null;

			return !firstContentReference.Equals(secondContentReference);
		}

		#endregion
	}
}