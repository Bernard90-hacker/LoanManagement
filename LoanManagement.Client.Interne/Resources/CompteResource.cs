﻿namespace LoanManagement.Client.Interne.Resources
{
    public class CompteResource
    {
        public int Id { get; set; }
        public string NumeroCompte { get; set; } = string.Empty;
        public int ClientId { get; set; }
    }
}
