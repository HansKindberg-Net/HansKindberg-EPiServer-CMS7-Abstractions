using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace EPiServer.Core
{
	[SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
	public class TypeMismatchException : Exception
	{
		#region Constructors

		public TypeMismatchException() {}
		public TypeMismatchException(string message) : base(message) {}
		public TypeMismatchException(string message, Exception innerException) : base(message, innerException) {}
		protected TypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) {}

		#endregion
	}
}