using System;

namespace EPiServer.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class IgnoreAttribute : Attribute {}
}