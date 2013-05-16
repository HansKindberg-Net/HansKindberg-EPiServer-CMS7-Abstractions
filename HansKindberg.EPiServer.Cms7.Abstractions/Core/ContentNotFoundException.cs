using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace EPiServer.Core
{
	[Serializable]
	[SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly")]
	public class ContentNotFoundException : EPiServerException
	{
		#region Fields

		private Guid _contentGuid;
		private ContentReference _contentLink;

		#endregion

		#region Constructors

		public ContentNotFoundException() : base("Content was not found") {}

		public ContentNotFoundException(ContentReference contentLink) : base(string.Format(CultureInfo.InvariantCulture, "Content with id {0} was not found", contentLink))
		{
			this._contentLink = contentLink;
		}

		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "guid")]
		public ContentNotFoundException(Guid contentGuid) : base(string.Format(CultureInfo.InvariantCulture, "Content with Guid \"{0}\" was not found", contentGuid))
		{
			this._contentGuid = contentGuid;
		}

		public ContentNotFoundException(string errorMessage) : base(errorMessage) {}
		protected ContentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {}
		public ContentNotFoundException(string message, Exception innerException) : base(message, innerException) {}

		#endregion

		#region Properties

		public Guid ContentGuid
		{
			get { return this._contentGuid; }
			set { this._contentGuid = value; }
		}

		public ContentReference ContentLink
		{
			get { return this._contentLink; }
			set { this._contentLink = value; }
		}

		#endregion
	}
}