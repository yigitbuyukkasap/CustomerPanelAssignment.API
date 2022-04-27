using DomainModels = CustomerPanelAssignment.API.Models.DomainModels;
using CustomerPanelAssignment.API.Models;
using DataAccess.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(DomainModels.User user)
        {
            var response = await _authRepository.Register(new User { Email = user.Email }, user.Password);
            if (response == 0)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(DomainModels.User user)
        {
            var response = await _authRepository.Login(
                user.Email, user.Password);

            if (response == "Kullanici bulunamadi" && response == "Yanlis Sifre")
                return BadRequest(response);

            return Ok(response);
        }
    }
}
