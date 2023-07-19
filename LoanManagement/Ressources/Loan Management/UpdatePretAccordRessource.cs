namespace LoanManagement.API.Ressources.Loan_Management
{
	public class UpdatePretAccordRessource
	{
		public int Id { get; set; }
		public double MontantPret { get; set; }
		public string DatePremiereEcheance { get; set; } = string.Empty;
		public string DateDerniereEcheance { get; set; } = string.Empty;
		public double MontantPrime { get; set; }
		public double Surprime { get; set; }
		public double SalaireNetMensuel { get; set; }
		public double Mensualite { get; set; } //Le montant qu'il doit payer à la fin de chaque mois.
		public string DateDepartRetraite { get; set; } = string.Empty;
		public int TypePretId { get; set; }
		public int PeriodicitePaiementId { get; set; }
		public int DossierClientId { get; set; }
	}
}
