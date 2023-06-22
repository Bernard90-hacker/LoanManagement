using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
namespace Constants.Utils
{
    public static class UtilsConstant
    {
        private static readonly Random Random = new();
		const int keySize = 64;
		const int iterations = 350000;


		public static string HashPassword(string password, out byte[] salt, out string passwordHashed)
		{
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			salt = RandomNumberGenerator.GetBytes(keySize);

			var hash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(password),
				salt,
				iterations,
				hashAlgorithm,
				keySize);
			passwordHashed = Convert.ToHexString(hash);

			return Convert.ToHexString(hash);
		}

		public static bool CheckPassword(string password, string hash, byte[] salt)
		{
			HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
			var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

			return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
		}

		public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException("Longueur non valide du hachage du mot de passe (64 octets attendus).", "storedHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Longueur non valide du sel de mot de passe (128 octets attendus).", "storedSalt");

            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != storedHash[i]).Any();
        }

		public static string CreateRefreshToken(string username, string refreshToken, out string refreshTokenTime)
		{
			List<Claim> claim = new()
			{
				new Claim(ClaimTypes.NameIdentifier, username.ToString())
			};
			SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(refreshToken));
			SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
			JwtSecurityToken? token = new(
					notBefore: DateTime.Now,
					expires: DateTime.Now.AddDays(7),
					signingCredentials: creds,
					claims: claim
				);

			string? jwt = new JwtSecurityTokenHandler().WriteToken(token);
            refreshTokenTime = token.ValidTo.ToString("dd/MM/yyyy");


            return jwt;

		}

		public static string IncrementStringWithNumbers(string str)
        {
            string digits = new(str.Where(char.IsDigit).ToArray());
            string letters = new(str.Where(char.IsLetter).ToArray());

            if (!int.TryParse(digits, out var number)) //int.Parse ferait le travail puisque seuls les chiffres sont sélectionnés
                throw new ArgumentException("Il s'est passé quelque chose d'inattendu");

            return letters + (++number).ToString("D5");
        }

        public static string IncrementNumbers(string str)
        {
            string digits = new(str.Where(char.IsDigit).ToArray());

            if (!int.TryParse(digits, out var number)) //int.Parse ferait le travail puisque seuls les chiffres sont sélectionnés
                throw new ArgumentException("Il s'est passé quelque chose d'inattendu");

            return (++number).ToString("D" + str.Count().ToString());
        }

        public static string UppercaseWords(string value)
        {
            return string.Join(" ", value.Split(' ').ToList()
                .ConvertAll(word => word[..1]
                .ToUpper() + word[1..].ToLower()));
        }

        public static string RandomString(int length)
        {
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GeneratePassword(int length, int nonAlphaNumericChars)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string allowedNonAlphaNum = "!@#$%^&*()_-+=[{]};:<>|./?";
            var pass = string.Empty;

            Random rd = new(DateTime.Now.Millisecond);
            for (var i = 0; i < length; i++)
            {
                if (nonAlphaNumericChars > 0)
                {
                    pass += allowedNonAlphaNum[rd.Next(allowedNonAlphaNum.Length)];
                    nonAlphaNumericChars--;
                }
                else
                    pass += allowedChars[rd.Next(allowedChars.Length)];
            }

            return pass;
        }

        public static string GetRandomCryptoPassword(int length)
        {
            var rgb = new byte[length];
            var rngCrypt = RandomNumberGenerator.Create();
            rngCrypt.GetBytes(rgb);
            var pass = Convert.ToBase64String(rgb);

            return pass;
        }

        public static string GetRandomCryptoPassword(int length, bool includeDigits, bool includeUppercase,
            bool includeLowercase, bool includeSpecialChars)
        {
            var pass = string.Empty;
            const string allowedDigits = "0123456789";
            const string allowedUppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string allowedLowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string allowedSpecialChars = "!@#$%^&*()_-+=[{]};:<>|./?";

            if (includeDigits)
                pass += allowedDigits;
            if (includeUppercase)
                pass += allowedUppercaseChars;
            if (includeLowercase)
                pass += allowedLowercaseChars;
            if (includeSpecialChars)
                pass += allowedSpecialChars;

            var res = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                var count = (int)Math.Ceiling(Math.Log(pass.Length, 2) / 8.0);
                Debug.Assert(count <= sizeof(uint));
                var offset = BitConverter.IsLittleEndian ? 0 : sizeof(uint) - count;
                var max = (int)(Math.Pow(2, count * 8) / pass.Length) * pass.Length;
                var uintBuffer = new byte[sizeof(uint)];

                while (res.Length < length)
                {
                    rng.GetBytes(uintBuffer, offset, count);
                    var num = BitConverter.ToUInt32(uintBuffer, 0);
                    if (num < max)
                    {
                        res.Append(pass[(int)(num % pass.Length)]);
                    }
                }
            }

            return res.ToString();
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }

        public static long GetColumnNumber(string columName)
        {
            var chars = columName.ToUpper().ToCharArray();
            var number = (long)(Math.Pow(26, chars.Length - 1))
                         * Convert.ToInt32(chars[0] - 64)
                         + (chars.Length > 2 ? GetColumnNumber(columName.Substring(1, columName.Length - 1))
                             : chars.Length == 2 ? Convert.ToInt32(chars[^1] - 64)
                             : 0);

            return number;
        }

        public static string GetColumnName(long columnNumber)
        {
            var val = new StringBuilder();

            for (var n = (int)(Math.Log(25 * (columnNumber + 1)) / Math.Log(26)) - 1; n >= 0; n--)
            {
                var x = (int)((Math.Pow(26, n + 1) - 1) / 25 - 1);
                if (columnNumber > x)
                    val.Append(Convert.ToChar((int)(((columnNumber - x - 1) / Math.Pow(26, n)) % 26 + 65)));
            }

            return val.ToString();
        }

        public static bool Compare<T>(T e1, T e2)
        {
            var flag = true;
            var match = false;
            int countFirst, countSecond;
            foreach (PropertyInfo propObj1 in e1.GetType().GetProperties())
            {
                var propObj2 = e2.GetType().GetProperty(propObj1.Name);
                if (propObj1.PropertyType.Name.Equals("List`1"))
                {
                    dynamic objList1 = propObj1.GetValue(e1, null);
                    dynamic objList2 = propObj2.GetValue(e2, null);
                    countFirst = objList1.Count;
                    countSecond = objList2.Count;
                    if (countFirst == countSecond)
                    {
                        countFirst = objList1.Count - 1;
                        while (countFirst > -1)
                        {
                            match = false;
                            countSecond = objList2.Count - 1;
                            while (countSecond > -1)
                            {
                                match = Compare(objList1[countFirst], objList2[countSecond]);
                                if (match)
                                {
                                    objList2.Remove(objList2[countSecond]);
                                    countSecond = -1;
                                    match = true;
                                }
                                if (match == false && countSecond == 0)
                                {
                                    return false;
                                }
                                countSecond--;
                            }
                            countFirst--;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (!(propObj1.GetValue(e1, null).Equals(propObj2.GetValue(e2, null))))
                {
                    flag = false;

                    return flag;
                }
            }

            return flag;
        }

        public static List<T> FindAll<T>(this IQueryable<T> vals, List<Predicate<T>> preds)
        {
            List<T> data = new();

            foreach (T e in vals)
            {
                bool pass = true;

                foreach (Predicate<T> p in preds)
                {
                    if (!(p(e)))
                    {
                        pass = false;
                        break;
                    }
                }

                if (pass) data.Add(e);
            }

            return data;
        }

        public static bool IsBetween(decimal d1, decimal value, decimal d2)
        {
            if (value > d1 && value < d1) return true;
            return false;
        }

		//public static string HashPassword(string password)
		//{
		//	using SHA256 sha256 = SHA256.Create();
		//	byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
		//	string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		//	return hashedPassword;
		//}
	}
}
