using Store.Business.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using System.Security.Claims;
using Store.API.Interfaces;

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

                using (var context = new StoreDataContext())
                {
                    var buyer = await context.Users.FirstOrDefaultAsync(us => us.Id.ToString() == userId);

                    Order order = new Order
                    {
                        Id = Guid.NewGuid(),
                        Buyer = buyer
                    };

                    foreach (var item in checkout.AppList)
                    {
                        var entityApp = new App
                        {
                            AppName = item.AppName,
                        };
                        order.AppList.Add(entityApp);
                    }

                    if (checkout.SaveCreditCardData == true)
                    {
                        var creditcard = await context.CreditCards.FirstOrDefaultAsync(c => c.CreditCardNumber == checkout.CreditCardNumber);

                        if (creditcard == null)
                        {
                            CreditCard card = new CreditCard
                            {
                                Id = Guid.NewGuid(),
                                CreditCardNumber = checkout.CreditCardNumber,
                                NameInCreditCard = checkout.NameInCreditCard,
                                ExpirationDate = checkout.ExpirationDate,
                                User = buyer
                            };
                            order.creditCard = card;
                        }
                    }

                    //_rabitMQProducer.SendProductMessage(order);
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();
                    _rabitMQProducer.SendProductMessage(checkout);

                    return Ok("Pedido registrado com sucesso!");
                }
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
