﻿namespace LoanManagement.core.Services.Users_Management
{
	public interface IEmployeService
	{
		Task<IEnumerable<Employe>> GetAll();
		Task<PagedList<Employe>> GetAll(EmployeParameters parameters);
		Task<Employe?> GetEmployeByEmail(string email);
		Task<PagedList<Employe>?> GetEmployesByDepartment(string codeDepartment);
		Task<Employe?> GetEmployeById(int id);
		Task<Utilisateur?> GetEmployeUserAccount(int userId);
		Task<Employe> Create(Employe employe);
		Task<Employe> Update(Employe employe, Employe employeToBeUpdated);
		Task<Employe> UpdateEmployeDepartment(Employe emp, int departementId);
		Task<Employe> UpdateEmployePhoto(Employe emp, string photo);
		Task Delete(Employe employe);
	}
}
