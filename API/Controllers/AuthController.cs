using API.Services.interfaces;
using DataAccess.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models.ViewModels.Auth;
using System.Security.Claims;
using Utilities.Handlers;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AuthController(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginVM loginDto)
        {
            var account = _accountRepository.GetByEmail(loginDto.Email);
            if (account is null)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Email atau password salah"
                });
            }

            var isPasswordValid = HashingHandler.ValidatePassword(loginDto.Password, account.Password);
            if (!isPasswordValid)
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "Email atau password salah"
                });
            }

            var claims = new List<Claim>()
            {
                new Claim("Guid", account.Guid.ToString()),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim(ClaimTypes.Email, loginDto.Email)
            };
            var token = "";
            try
            {
                token = _tokenService.GenerateToken(claims);
                return Ok(new ResponseVM
                {
                    IsSuccess = true,
                    Message = "Berhasil login",
                    Data = new TokenVM { Token = token }
                });
            }
            catch
            {
                return BadRequest(new ResponseVM
                {
                    IsSuccess = false,
                    Message = "gagal membuat token"
                });
            }
        }
    }
}
