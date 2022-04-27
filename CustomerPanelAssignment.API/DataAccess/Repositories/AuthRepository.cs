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

        public Task<string> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Register(User user, string password)
        {

            if (await UserExists(user.Email))
                return 0;


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt= passwordSalt;

            
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
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
