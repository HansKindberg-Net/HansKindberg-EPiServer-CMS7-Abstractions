using System;
using System.Diagnostics.CodeAnalysis;
using EPiServer.Core;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions.Data // ReSharper restore CheckNamespace
{
	internal static class Validator
	{
		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateWritableClone")]
		internal static void ValidateNotReadOnly(IReadOnly readOnly)
		{
			if(readOnly == null)
				throw new ArgumentNullException("readOnly");

			if(readOnly.IsReadOnly)
				throw new NotSupportedException("Invalid use of read-only object, call CreateWritableClone() to create a writable clone.");
		}

		#endregion
	}
}