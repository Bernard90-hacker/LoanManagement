﻿namespace LoanManagement.API.Ressources.Users_Management
{
	public class EmployeRessource
	{
		public string Nom { get; set; } = string.Empty;
		public string Prenoms { get; set; } = string.Empty;
		public string Matricule { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public int DepartementId { get; set; }
	}
}
