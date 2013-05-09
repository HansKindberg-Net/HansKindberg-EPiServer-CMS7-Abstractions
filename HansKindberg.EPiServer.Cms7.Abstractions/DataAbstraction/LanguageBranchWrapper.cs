using System;
using System.Collections.Generic;
using System.Globalization;

namespace EPiServer.DataAbstraction
{
	public class LanguageBranchWrapper : ILanguageBranchRepository
	{
		#region Methods

		public virtual void Delete(int languageBranchId)
		{
			LanguageBranch.Load(languageBranchId).Delete();
		}

		public virtual IList<LanguageBranch> ListAll()
		{
			return LanguageBranch.ListAll();
		}

		public virtual IList<LanguageBranch> ListEnabled()
		{
			return LanguageBranch.ListEnabled();
		}

		public virtual LanguageBranch Load(CultureInfo culture)
		{
			return LanguageBranch.Load(culture);
		}

		public virtual LanguageBranch Load(int id)
		{
			return LanguageBranch.Load(id);
		}

		public virtual LanguageBranch LoadFirstEnabledBranch()
		{
			return LanguageBranch.LoadFirstEnabledBranch();
		}

		public virtual void Save(LanguageBranch languageBranch)
		{
			if(languageBranch == null)
				throw new ArgumentNullException("languageBranch");

			languageBranch.Save();
		}

		#endregion
	}
}