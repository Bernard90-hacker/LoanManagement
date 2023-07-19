namespace LoanManagement.Data.SqlServer.Configuration.Loan_Management
{
	public class ParametrageFraisDossier
	{
        public int Id { get; set; }
        public double Plancher { get; set; }
        public double Plafond { get; set; }
        public int PourcentageCommissionEngagement { get; set; }
        public double FraisFixe { get; set; }
        public double FraisDossiers { get; set; }
        public int PourcentageTAF { get; set; }
    }
}

