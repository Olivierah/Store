using Microsoft.EntityFrameworkCore;
using Store.Repository.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Context;
using SecureIdentity.Password;
using Store.API.Services;
using Store.Domain.Dtos;

namespace Store.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly TokenService _tokenService;

        // Dependências
        public AccountController(TokenService tokenService, ILogger<AppController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }


        // Endpoints
        [HttpPost("v1/account/")]
        public async Task<IActionResult> CreateAccount([FromBody] UserDto userDto)
        {
            // verifica se já existe um cadastro com o email ou cpf informado
            if (AccountDataAccess.NewAccountVerifyer(userDto) == true) 
                return BadRequest("CPF ou E-mail já cadastrado");


            try
            {
                AccountDataAccess.CreateNewAccount(userDto);
                return Ok("Cadastro realizado com sucesso!");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "ACX98");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(400, "ACX99");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "ACX05 - Falha interna do servidor");
            }
        }

        [HttpPost("v1/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] StoreDataContext context)
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
                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "ACX98");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

    }
}

