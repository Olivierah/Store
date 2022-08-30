using System.Text.RegularExpressions;
using Store.Domain.Dtos;
using Store.Repository.DataAccess;
using Store.Domain.Entities;

namespace Store.Business.Utilities
{
    public class CheckoutUtilities
    {
        public static bool CreditCardInfoValidation(string cardNo, string expiryDate, string cvv)
        {
            var cardCheck = new Regex(@"^[1-9][0-9]{2}-[1-3]{3}-[0-9]{3}-[0-9]{3}$");
            var monthCheck = new Regex(@"^(0[1-9]|10|11|12)$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");
            var cvvCheck = new Regex(@"^[0-9]{3,4}$");

            if (!cardCheck.IsMatch(cardNo)) // Verifica se o número do cartão é válido
                return false;
            if (!cvvCheck.IsMatch(cvv)) // Verifica se o cvv é válido
                return false;

            var dateParts = expiryDate.Split('/'); //Prazo de validade a partir de MM/yyyy            
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1]))
                return false; // Verifica o formato de data "MM/yyyy"

            var year = int.Parse(dateParts[1]);
            var month = int.Parse(dateParts[0]);
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //Recupera a data de validade
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);


            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));  //Verifica se a data de validade do cartão é maior que a data de hoje e nos próximos 6 anos
        }
    }
}
