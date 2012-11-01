using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace EPiServer.Core
{
	[Serializable]
	public abstract class ContentData : IContentData, IModifiedTrackable, IReadOnly
	{
		#region Constructors

		protected ContentData() : this(new PropertyDataCollection()) {}

		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected ContentData(PropertyDataCollection properties)
		{
			EPiServer.Framework.Validator.ThrowIfNull("properties", properties);
			// ReSharper disable DoNotCallOverridableMethodsInConstructor
			this.Properties = properties;
			// ReSharper restore DoNotCallOverridableMethodsInConstructor
		}

		#endregion

		#region Properties

		bool IModifiedTrackable.IsModified
		{
			get { return this.IsModified; }
		}

		protected virtual bool IsModified
		{
			get { return this.Properties != null && this.Properties.Any(p => p.IsModified); }
		}

		bool IContentData.IsNull
		{
			get { return this.IsNull; }
		}

		protected virtual bool IsNull
		{
			get
			{
				if(this.Properties != null)
					return !this.Properties.Any();

				return true;
			}
		}

		public virtual bool IsReadOnly { get; protected set; }

		public virtual object this[string index]
		{
			get
			{
				PropertyData propertyData = this.Properties != null ? this.Properties[index] : null;
				return propertyData != null ? propertyData.Value : null;
			}
			set { this.SetValue(index, value); }
		}

		PropertyDataCollection IContentData.Properties
		{
			get { return this.Properties; }
		}

		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		protected virtual PropertyDataCollection Properties { get; set; }

		#endregion

		#region Methods

		public object CreateWritableClone()
		{
			return this.CreateWriteableCloneImplementation();
		}

		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Writeable")]
		protected virtual object CreateWriteableCloneImplementation()
		{
			ContentData contentData = (ContentData) this.MemberwiseClone();
			contentData.IsReadOnly = false;

			if(this.Properties != null)
				contentData.Properties = this.Properties.CreateWritableClone();

			return contentData;
		}

		public virtual object GetValue(string name)
		{
			PropertyData propertyData = this.Properties != null ? this.Properties.Get(name) : null;
			return propertyData == null ? null : propertyData.Value;
		}

		public virtual void MakeReadOnly()
		{
			if(this.IsReadOnly)
				return;

			if(this.Properties != null)
				this.Properties.MakeReadOnly();

			this.IsReadOnly = true;
		}

		void IModifiedTrackable.ResetModified()
		{
			this.ResetModified();
		}

		protected virtual void ResetModified()
		{
			this.ThrowIfReadOnly();

			if(this.Properties == null)
				return;

			foreach(PropertyData propertyData in this.Properties)
			{
				propertyData.IsModified = false;
			}
		}

		public virtual void SetValue(string index, object value)
		{
			PropertyData propertyData = this.Properties != null ? this.Properties.Get(index) : null;

			if(propertyData == null)
				throw new EPiServerException(string.Format(CultureInfo.InvariantCulture, "Property '{0}' does not exist, can only assign values to existing properties", index ?? string.Empty));

			propertyData.Value = value;
		}

		protected virtual void ThrowIfReadOnly()
		{
			HansKindberg.EPiServer.Cms7.Abstractions.Data.Validator.ValidateNotReadOnly(this);
		}

		#endregion
	}
}