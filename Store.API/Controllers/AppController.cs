using Microsoft.AspNetCore.Mvc;
using Store.Business.AppUtilities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Store.API.Controllers
{
    [Authorize]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpGet("v1/app/")]
        public async Task<IActionResult> GetAllApps()
        {
            try
            {
                var appList = AppUtilities.ValidateIfDataAlreadyExist();
                return Ok(appList);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "ACX98 - Falha interna do servidor");
            }
            catch
            {
                return StatusCode(500, "ACX05 - Falha interna do servidor");
            }
        }
    }
}
