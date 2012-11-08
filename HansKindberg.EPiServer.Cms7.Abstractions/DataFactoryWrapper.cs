using System;
using EPiServer;

// ReSharper disable CheckNamespace

namespace HansKindberg.EPiServer.Cms7.Abstractions // ReSharper restore CheckNamespace
{
	public class DataFactoryWrapper
	{
		#region Fields

		private readonly DataFactory _dataFactory;

		#endregion

		#region Constructors

		public DataFactoryWrapper(DataFactory dataFactory)
		{
			if(dataFactory == null)
				throw new ArgumentNullException("dataFactory");

			this._dataFactory = dataFactory;
		}

		#endregion

		#region Properties

		protected internal DataFactory DataFactory
		{
			get { return this._dataFactory; }
		}

		#endregion
	}
}