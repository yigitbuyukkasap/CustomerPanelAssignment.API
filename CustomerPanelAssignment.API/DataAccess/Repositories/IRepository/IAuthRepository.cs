using CustomerPanelAssignment.API.Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<int> Register(User user, string password);
        Task<string> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
