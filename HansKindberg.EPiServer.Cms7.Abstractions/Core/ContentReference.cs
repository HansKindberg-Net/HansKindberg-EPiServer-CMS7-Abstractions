using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.EPiServer.Cms7.Abstractions.Core;

namespace EPiServer.Core
{
	public abstract class ContentReference : IComparable, EPiServer.Data.Entity.IReadOnly
	{
		#region Fields

		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")] public static readonly ContentReference EmptyReference = new PageReferenceWrapper(PageReference.EmptyReference);
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")] public static readonly ContentReference SelfReference = new PageReferenceWrapper(PageReference.SelfReference);

		#endregion

		#region Properties

		public abstract bool GetPublishedOrLatest { get; }
		public static ContentReference GlobalBlockFolder { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract int ID { get; set; }

		// ReSharper restore InconsistentNaming
		public abstract bool IsExternalProvider { get; }
		public abstract bool IsReadOnly { get; }
		public abstract string ProviderName { get; set; }
		public static PageReference RootPage { get; set; }
		public static ContentReference SiteBlockFolder { get; set; }
		public static PageReference StartPage { get; set; }

		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "WasteBasket")]
		public static PageReference WasteBasket { get; set; }

		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract int WorkID { get; set; }

		#endregion

		#region Methods

		public abstract int CompareTo(object obj);
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		public abstract bool CompareToIgnoreWorkID(ContentReference contentReference);

		// ReSharper restore InconsistentNaming
		public abstract ContentReference Copy();
		public abstract ContentReference CreateReferenceWithoutVersion();
		public abstract object CreateWritableClone();

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override bool Equals(object obj)
		{
			throw new NotImplementedException("The method must be implemented in the derived class.");
		}

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override int GetHashCode()
		{
			throw new NotImplementedException("The method must be implemented in the derived class.");
		}

		public abstract void MakeReadOnly();
		public abstract ContentReference ParseReference(string complexReference);

		private static void ThrowArgumentNullExceptionIfAnyParameterIsNull(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			if(ReferenceEquals(firstContentReference, null))
				throw new ArgumentNullException("firstContentReference");

			if(ReferenceEquals(secondContentReference, null))
				throw new ArgumentNullException("secondContentReference");
		}

		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public override string ToString()
		{
			throw new NotImplementedException("The method must be implemented in the derived class.");
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

		public static bool operator >(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			ThrowArgumentNullExceptionIfAnyParameterIsNull(firstContentReference, secondContentReference);

			return firstContentReference.CompareTo(secondContentReference) == 1;
		}

		public static bool operator !=(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			return !(firstContentReference == secondContentReference);
		}

		public static bool operator <(ContentReference firstContentReference, ContentReference secondContentReference)
		{
			ThrowArgumentNullExceptionIfAnyParameterIsNull(firstContentReference, secondContentReference);

			return firstContentReference.CompareTo(secondContentReference) == -1;
		}

		#endregion
	}
}