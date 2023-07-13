﻿namespace LoanManagement.core.Services.Loan_Management
{
	public interface IPretAccordService
	{
		Task<IEnumerable<PretAccord>> GetAll();
		Task<PretAccord> GetPretAccordForDossier(int dossierId);
		Task<PretAccord> GetById(int id);
		Task<PretAccord> Create(PretAccord pret);
		Task<PretAccord> Update(PretAccord pUpdated, PretAccord p);
		Task Delete(PretAccord p);
	}
}
