﻿namespace LoanManagement.API.Ressources.Users_Management
{
	public class ApplicationRessource
	{
		public string Logo { get; set; } = string.Empty;
		public string Libelle { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Version { get; set; } = string.Empty;
		public int Statut { get; set; }
		public int ApplicationId { get; set; }
		public string ApplicationCode { get; set; } = string.Empty;
    }
}
