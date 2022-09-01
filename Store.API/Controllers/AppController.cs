using Microsoft.AspNetCore.Authorization;
using Store.Business.AppUtilities;
using Microsoft.AspNetCore.Mvc;

namespace Store.API.Controllers
{
    [Authorize]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly ILogger _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        [HttpGet("v1/app/")]
        public async Task<IActionResult> GetAllApps()
        {
            try
            {
                var appList = AppUtilities.ValidateIfDataAlreadyExist();
                return Ok(appList);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "ACX98 - Falha interna do servidor");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "ACX05 - Falha interna do servidor");
            }
        }
    }
}
