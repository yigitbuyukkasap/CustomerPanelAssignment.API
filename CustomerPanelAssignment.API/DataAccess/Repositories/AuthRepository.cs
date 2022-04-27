using CustomerPanelAssignment.API.Models;
using DataAccess.Data;
using DataAccess.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));

            //  1 KUllanici yoksa - Bulunamadi
            //  2 Girilen Sifre Ile Hash uyusmaz ise - Yanlis Sifre
            //  3 Her sey gectigi durum da - User Id Doner
            if (user == null)
                return "Kullanici bulunamadi";
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return "Yanlis Sifre";
            else
                return user.Id.ToString();
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
    }
}
