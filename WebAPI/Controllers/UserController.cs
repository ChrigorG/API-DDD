using Application.Interface;
using Entities.Entity;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Shared;
using System.Text;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly UserManager<UserEntity> _userManager;


        public UserController(IUserApplication userApplication,
            UserManager<UserEntity> userManager)
        {
            _userApplication = userApplication;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] LoginDTO login)
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
    }
}
