namespace LoanManagement.core.Models.Users_Management
{
	public class ParamGlobal
	{
        public int Id { get; set; }
        public bool IncludeDigits { get; set; }
        public bool IncludeLowerCase { get; set; }
        public bool IncludeUpperCase { get; set; }
        public bool IncludeSpecialCharacters { get; set; }
        public bool ExcludeUsername { get; set; }
        public int Taille { get; set; }
        public int DelaiExpiration { get; set; } //Jours
      

    }
}
