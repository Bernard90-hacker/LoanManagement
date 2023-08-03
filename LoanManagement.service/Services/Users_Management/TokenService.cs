namespace LoanManagement.service.Services.Users_Management
{
	public class TokenService : ITokenService
	{
		public TokenService() { }

        public string GenerateAccessToken(string secret, IEnumerable<Claim> claims, string issuer = null, string audience = null)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),  
                signingCredentials: signingCredentials,
                issuer: issuer,
                audience: audience);
            var jwtToken = jwtTokenHandler.WriteToken(tokenOptions);

            return jwtToken;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string secret, string token)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false, //Vous pouver vouloir valider le public et l'émetteur en fonction de votre cas d'utilisation
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = false //Ici, nous disons que nous ne soucions pas de la date d'expiration du jeton
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var principal = jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken.IsNull() || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token invalide");

            return principal;
        }
    }
}
