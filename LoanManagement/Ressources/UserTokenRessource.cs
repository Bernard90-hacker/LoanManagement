﻿namespace LoanManagement.API.Ressources
{
	public class UserTokenRessource
	{

		public int Id { get; set; }
		public int UserId { get; set; }
		public string Token { get; set; } = default!;
		public DateTime CreatedAt { get; set; }
		public DateTime ExpiredAt { get; set; }
	}
}
