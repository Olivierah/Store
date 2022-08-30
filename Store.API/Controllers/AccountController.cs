using Store.Domain.Dtos;
using Store.API.Services;
using Store.Domain.Entities;
using SecureIdentity.Password;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Store.Repository.DataAccess;

namespace Store.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly TokenService _tokenService;

        // Dependências
        public AccountController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }
        // Endpoints
        [HttpPost("v1/account/")]
        public async Task<IActionResult> CreateAccount([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Usuário ou e-mail já cadastrado");

            try
            {
                AccountDataAccess.CreateNewAccount(userDto);
                return Ok("Cadastro realizado com sucesso!");
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "ACX98");
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "ACX99");
            }
            catch
            {
                return StatusCode(500, "ACX05 - Falha interna do servidor");
            }
        }

        [HttpPost("v1/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] StoreDataContext context, [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest("ACX097 - Usuário ou senha inválido");

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == login.Email);

            if (user == null)
                return StatusCode(401, "ACX096 - Usuário ou senha inválido");

            var temp = PasswordHasher.Verify(user.PasswordHash, login.Password);


            if (!PasswordHasher.Verify(user.PasswordHash, login.Password))
                return StatusCode(401, "ACX095 - Usuário ou senha inválido");

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "ACX98 - limite de conexão excedido");
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}

