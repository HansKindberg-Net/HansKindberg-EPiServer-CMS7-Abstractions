using System.Collections.Generic;
using System.Globalization;

namespace EPiServer.DataAbstraction
{
	public interface ILanguageBranchRepository
	{
		#region Methods

		void Delete(int languageBranchId);
		IList<LanguageBranch> ListAll();
		IList<LanguageBranch> ListEnabled();
		LanguageBranch Load(CultureInfo culture);
		LanguageBranch Load(int id);
		LanguageBranch LoadFirstEnabledBranch();
		void Save(LanguageBranch languageBranch);

		#endregion
	}
}