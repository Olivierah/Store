using Store.Domain.Entities;
using Store.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Store.Repository.DataAccess
{
    public class CheckoutDataAccess
    {
        public static User GetUserbyID(string userId)
        {
            using(var context = new StoreDataContext())
            {
                var buyer = context.Users.FirstOrDefault(us => us.Id.ToString() == userId);
                return buyer;
            }           
        } 

        public static CreditCard VerifyIfCreditCardAlreadyExist(string CreditCardNumber)
        {
            using (var context = new StoreDataContext())
            {
                var creditcard = context.CreditCards.FirstOrDefault(c => c.CreditCardNumber == CreditCardNumber);
                return creditcard;
            }              
        }

        public async static void SaveNewOrder(Order order)
        {
            using (var context = new StoreDataContext())
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }
    }

}
