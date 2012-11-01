using System;
using System.ComponentModel;
using System.Globalization;

namespace EPiServer.Core
{
	public class ContentReferenceConverter<T> : TypeConverter where T : ContentReference
	{
		#region Methods

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (sourceType == typeof(string) || base.CanConvertFrom(context, sourceType));
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (destinationType == typeof(string) || base.CanConvertTo(context, destinationType));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value == null)
				return ContentReference.EmptyReference;

			string stringValue = value as string;

			if(stringValue != null)
			{
				bool emptyReference = stringValue.Trim().Length == 0;

				if(typeof(T) == typeof(PageReference))
					return emptyReference ? PageReference.EmptyReference : PageReference.Parse(stringValue);

				return emptyReference ? ContentReference.EmptyReference : ContentReference.Parse(stringValue);
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if(value != null && !(value is ContentReference))
				throw new ArgumentException("Invalid ContentReference", "value");

			if(destinationType != typeof(string))
				return base.ConvertTo(context, culture, value, destinationType);

			return value == null ? string.Empty : value.ToString();
		}

		#endregion
	}
}