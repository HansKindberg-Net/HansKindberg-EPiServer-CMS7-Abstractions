using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace EPiServer.Core
{
	[SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
	public abstract class ContentReference : IComparable, EPiServer.Data.Entity.IReadOnly
	{
		#region Properties

		public abstract bool GetPublishedOrLatest { get; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract int ID { get; set; }

		// ReSharper restore InconsistentNaming
		public abstract bool IsExternalProvider { get; }
		public abstract bool IsReadOnly { get; }
		public abstract string ProviderName { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract int WorkID { get; set; }

		#endregion

		#region Methods

		public virtual int CompareTo(object obj)
		{
			ContentReference contentReference = obj as ContentReference;

			if(contentReference == null)
				throw new ArgumentException("Object is not an ContentReference");

			if(this == contentReference)
				return 0;

			if(this.ID > contentReference.ID)
				return 1;

			return -1;
		}

		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract bool CompareToIgnoreWorkID(ContentReference contentReference);

		// ReSharper restore InconsistentNaming
		public abstract ContentReference Copy();
		public abstract ContentReference CreateReferenceWithoutVersion();
		public abstract object CreateWritableClone();

		public override bool Equals(object obj)
		{
			return this == (obj as ContentReference);
		}

		public override int GetHashCode()
		{
			return this.ID + this.WorkID + (this.ProviderName == null ? 0 : this.ProviderName.GetHashCode());
		}

		public abstract void MakeReadOnly();
		public abstract ContentReference ParseReference(string complexReference);

		public override string ToString()
		{
			if(this.ID == 0)
				return this.WorkID == -1 ? "-" : string.Empty;

			string contentReferenceString = this.ID.ToString(CultureInfo.InvariantCulture);

			if(this.WorkID != 0)
				contentReferenceString = contentReferenceString + "_" + this.WorkID.ToString(CultureInfo.InvariantCulture);

			if(this.ProviderName == null)
				return contentReferenceString;

			if(this.WorkID == 0)
				return contentReferenceString + "__" + this.ProviderName;

			return contentReferenceString + "_" + this.ProviderName;
		}

		#endregion

		#region Operators

		public static bool operator ==(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			if(ReferenceEquals(firstContentReference, secondContentReference))
				return true;

			if(ReferenceEquals(firstContentReference, null) || ReferenceEquals(secondContentReference, null))
				return false;

			return firstContentReference.ID == secondContentReference.ID && firstContentReference.WorkID == secondContentReference.WorkID && firstContentReference.ProviderName == secondContentReference.ProviderName;
		}

		public static bool operator !=(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			return !(firstContentReference == secondContentReference);
		}

		#endregion
	}
}