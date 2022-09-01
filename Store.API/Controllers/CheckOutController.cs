using Store.Domain.Dtos;
using Store.API.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Store.Business.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Store.API.Controllers
{
    [Authorize]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly IRabitMQPublisher _rabitMQProducer;
        public CheckOutController(IRabitMQPublisher rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }


        [HttpPost("v1/checkout/")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDto checkout)
        {
            try
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Valida cartão de crédito
                if (CheckoutUtilities.CreditCardInfoValidation(checkout.CreditCardNumber, checkout.ExpirationDate, checkout.Cvv) == false)
                {
                    return BadRequest("Cartão de crédito inválido!");
                }
                // Valida carrinho de compras
                if (checkout.AppList.Count == 0)
                {
                    return BadRequest("Seu carrinho está vazio!");
                }

                _rabitMQProducer.SendProductMessage(checkout, userId);
                return Ok("Pedido registrado com sucesso!");
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "ACX98");
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, "ACX99 - Não foi possível concluir a compra");
            }
            catch
            {
                return StatusCode(500, "ACX05 - Falha interna do servidor");
            }
        }
    }
}
