using CustomerPanelAssignment.API.Models;
using DataAccess.Data;
using DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public AuthRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            //   KUllanici yoksa - Bulunamadi
            //   Girilen Sifre Ile Hash uyusmaz ise - Yanlis Sifre
            //   Her sey gectigi durum da - User Id Doner
            if (user == null)
                return "Kullanici bulunamadi";
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return "Yanlis Sifre";
            else
                return CreateToken(user);
        }

        public async Task<int> Register(User user, string password)
        {

            if (await UserExists(user.Email))
                return 0;


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;


            _db.User.Add(user);
            var result = await _db.SaveChangesAsync();

            return result;

        }

        public async Task<bool> UserExists(string email)
        {
            if (await _db.User.AnyAsync(u =>
                    u.Email.ToLower()
                    .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }


        // Creating passwordhash
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;

                }
                return true;
            }

        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email) 
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
