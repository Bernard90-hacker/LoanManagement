namespace LoanManagement.core.Models.Loan_Management
{
	public class PretAccord
	{
        public int Id { get; set; }
        public double MontantPret { get; set; }
        public string DatePremiereEcheance { get; set; } = string.Empty;
        public string DateDerniereEcheance { get; set; } = string.Empty;
        public double MontantPrime { get; set; }
        public double Surprime { get; set; }
        public double PrimeTotale { get; set; }
        public double SalaireNetMensuel { get; set; }
        public double QuotiteCessible { get; set; }
        public double Mensualite { get; set; } //Le montant qu'il doit payer à la fin de chaque mois.
        public int TauxEngagement { get; set; }
        public string DateDepartRetraite { get; set; } = string.Empty;
        public int TypeContratId { get; set; }
        public TypeContrat? TypeContrat { get; set; }
    }
}
