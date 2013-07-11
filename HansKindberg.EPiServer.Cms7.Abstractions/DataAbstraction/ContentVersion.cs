using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using EPiServer.Core;

namespace EPiServer.DataAbstraction
{
	[DebuggerDisplay("{ContentLink}, {Name}, {LanguageBranch}, {Status}, {IsCommonDraft}")]
	[Serializable]
	public class ContentVersion : IReadOnly<object>
	{
		#region Fields

		private ContentReference _contentLink;
		private bool _isCommonDraft;
		private bool _isMasterLanguageBranch;
		private string _languageBranch;
		private int _masterVersionId;
		private string _name;
		private DateTime _saved;
		private string _savedBy;
		private VersionStatus _status;
		private string _statusChangedBy;

		#endregion

		#region Constructors

		public ContentVersion(ContentReference contentLink, string name, VersionStatus workStatus, DateTime saved, string savedBy, string statusChangedBy, int masterVersionId, string languageBranch, bool isMasterLanguageBranch, bool isCommonDraft)
		{
			this._contentLink = contentLink;
			this._name = name;
			this._status = workStatus;
			this._saved = saved;
			this._savedBy = savedBy;
			this._statusChangedBy = statusChangedBy ?? savedBy;
			this._masterVersionId = masterVersionId;
			this._languageBranch = languageBranch;
			this._isMasterLanguageBranch = isMasterLanguageBranch;
			this._isCommonDraft = isCommonDraft;
		}

		#endregion

		#region Properties

		public ContentReference ContentLink
		{
			get { return this._contentLink; }
			set
			{
				this.ThrowIfReadOnly();
				this._contentLink = value;
			}
		}

		public bool IsCommonDraft
		{
			get { return this._isCommonDraft; }
			set
			{
				this.ThrowIfReadOnly();
				this._isCommonDraft = value;
			}
		}

		public bool IsMasterLanguageBranch
		{
			get { return this._isMasterLanguageBranch; }
			set
			{
				this.ThrowIfReadOnly();
				this._isMasterLanguageBranch = value;
			}
		}

		[XmlIgnore]
		public bool IsReadOnly { get; private set; }

		public string LanguageBranch
		{
			get { return this._languageBranch; }
			set
			{
				this.ThrowIfReadOnly();
				this._languageBranch = value;
			}
		}

		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public int MasterVersionID // ReSharper restore InconsistentNaming
		{
			get { return this._masterVersionId; }
			set
			{
				this.ThrowIfReadOnly();
				this._masterVersionId = value;
			}
		}

		public string Name
		{
			get { return this._name; }
			set
			{
				this.ThrowIfReadOnly();
				this._name = value;
			}
		}

		public DateTime Saved
		{
			get { return this._saved; }
			set
			{
				this.ThrowIfReadOnly();
				this._saved = value;
			}
		}

		public string SavedBy
		{
			get { return this._savedBy; }
			set
			{
				this.ThrowIfReadOnly();
				this._savedBy = value;
			}
		}

		public VersionStatus Status
		{
			get { return this._status; }
			set
			{
				this.ThrowIfReadOnly();
				this._status = value;
			}
		}

		public string StatusChangedBy
		{
			get { return this._statusChangedBy; }
			set
			{
				this.ThrowIfReadOnly();
				this._statusChangedBy = value;
			}
		}

		#endregion

		#region Methods

		public ContentVersion CreateWritableClone()
		{
			ContentVersion contentVersion = (ContentVersion) this.MemberwiseClone();
			contentVersion.IsReadOnly = false;
			contentVersion.ContentLink = (ContentReference) this.ContentLink.CreateWritableClone();
			return contentVersion;
		}

		object IReadOnly<object>.CreateWritableClone()
		{
			return this.CreateWritableClone();
		}

		public override bool Equals(object obj)
		{
			ContentVersion contentVersion = obj as ContentVersion;

			if(contentVersion != null)
				return this.ContentLink.Equals(contentVersion.ContentLink);

			PageVersion pageVersion = obj as PageVersion;

			// ReSharper disable SuspiciousTypeConversion.Global
			return pageVersion != null && this.ContentLink.Equals(pageVersion.ID);
			// ReSharper restore SuspiciousTypeConversion.Global
		}

		public static ContentVersion FromPageVersion(PageVersion pageVersion)
		{
			return pageVersion;
		}

		public override int GetHashCode()
		{
			return this.ContentLink.GetHashCode();
		}

		public void MakeReadOnly()
		{
			this.IsReadOnly = true;
			this.ContentLink.MakeReadOnly();
		}

		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateWritableClone")]
		protected internal virtual void ThrowIfReadOnly()
		{
			if(this.IsReadOnly)
				throw new NotSupportedException("Invalid use of read-only object, call CreateWritableClone() to create a writable clone.");
		}

		#endregion

		#region Operators

		public static bool operator ==(ContentVersion firstContentVersion, ContentVersion secondContentVersion)
		{
			if(ReferenceEquals(firstContentVersion, secondContentVersion))
				return true;

			if(ReferenceEquals(firstContentVersion, null) || ReferenceEquals(secondContentVersion, null))
				return false;

			return firstContentVersion.Equals(secondContentVersion);
		}

		public static bool operator !=(ContentVersion firstContentVersion, ContentVersion secondContentVersion)
		{
			return !(firstContentVersion == secondContentVersion);
		}

		#endregion

		#region Implicit operator

		public static implicit operator ContentVersion(PageVersion pageVersion)
		{
			return pageVersion == null ? null : new ContentVersion(pageVersion.ID, pageVersion.Name, pageVersion.Status, pageVersion.Saved, pageVersion.SavedBy, pageVersion.StatusChangedBy, pageVersion.MasterVersionID, pageVersion.LanguageBranch, pageVersion.IsMasterLanguageBranch, false);
		}

		#endregion
	}
}