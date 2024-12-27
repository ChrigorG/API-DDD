using Application.Interface;
using Entities.Entity;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Shared;
using System.Text;
using WebAPI.Models;
using WebAPI.Token;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserController(IUserApplication userApplication,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            _userApplication = userApplication;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Email or Password is null or empty");
            }

            // Por não está diretamente ligado a um Browser a API, o terceiro paramentro vai ser falso (CheckBox Manter usuario logado).
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) 
            {
                return NotFound("User not fould");
            }

            var token = TokenUser();

            return Ok(token.Value);
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("CreateUserIdentity")]
        public async Task<IActionResult> CreateUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password) || Helper.AgeIsNullOrMinusOrEqualZero(login.Age) || string.IsNullOrWhiteSpace(login.CellPhone))
            {
                return BadRequest("Invalid data");
            }

            UserEntity user = new UserEntity
            {
                UserName = login.Email,
                Email = login.Email,
                Age = login.Age,
                CellPhone = login.CellPhone,
                Types = TypesUser.Common
            };

            var exist = await _userApplication.EmailExists(login.Email);
            if (exist)
            {
                return Conflict("This user already exist");
            }

            var result = await _userManager.CreateAsync(user, login.Password);
            if (result.Errors.Any())
            {
                return Conflict(result.Errors);
            }

            // Geração de codigo de confirmação (O usuario pode confirmar por exemplo no Email dele)
            var userId = user.Id;
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Mas irei retornar por aqui mesmo a confirmação
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                return Problem("Unable to confirm user creation");
            }

            return Created(string.Empty, "Created user with success");
        }

        private TokenJWT TokenUser()
        {
            return new TokenJWTBuilder()
                .AddSecurityKey(JWTSecurityKey.Create(Const.SecretKeyToken))
                .AddSubject("Project - API-DDD")
                .AddIssuer(Const.TestSecurityBearer)
                .AddAudience(Const.TestSecurityBearer)
                .AddClaim(Const.UserIdLoggedAPI, "1")
                .AddExpiry(Const.OneHourInMinutes)
                .Builder();
        }
    }
}
