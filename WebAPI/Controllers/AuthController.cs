using Application.Interface;
using Entities.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared;
using WebAPI.DTO;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public AuthController(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginDTO login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Email or Password is null or empty");
            }

            // Por não está diretamente ligado a um Browser a API, o terceiro paramentro vai ser falso (CheckBox Manter usuario logado).
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return NotFound("Invalid email or password");
            }

            // Busca o usuário pelo e-mail para obter o Id diretetamente no Banco de Dados
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = this.GetToken(user.Id);

            string userId = User.FindFirst(Const.UserIdLoggedAPI)?.Value ?? string.Empty;

            return Ok(token.Value);
        }

        private TokenJWT GetToken(string idUser)
        {
            return new TokenJWTBuilder()
                .AddSecurityKey(JWTSecurityKey.Create(Const.SecretKeyToken))
                .AddSubject("Project - API-DDD")
                .AddIssuer(Const.TestSecurityBearer)
                .AddAudience(Const.TestSecurityBearer)
                .AddClaim(Const.UserIdLoggedAPI, idUser)
                .AddExpiry(Const.OneHourInMinutes)
                .Builder();
        }
    }
}