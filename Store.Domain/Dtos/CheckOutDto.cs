using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Dtos
{
    public class CheckOutDto
    {
        [Required]
        public List<AppsDto> AppList { get; set; }

        // Dados do Cartão
        [Required]
        [RegularExpression(@"^[1-9][0-9]{2}-[1-3]{3}-[0-9]{3}-[0-9]{3}$")]
        [CreditCard(ErrorMessage = "Cartão de crédito inválido")]
        public string CreditCardNumber { get; set; }


        
        [Required(ErrorMessage = "Digite o nome que aparece no cartão de crédito")]
        [MaxLength(25)]
        public string NameInCreditCard { get; set; }



        [Required]
        [RegularExpression(@"(0[1-9]|10|11|12)/20[0-9]{2}$")]
        public string ExpirationDate { get; set; }



        [Required]
        [RegularExpression(@"^[0-9]{3,4}$")]
        public string Cvv { get; set; }



        [Required]
        public bool SaveCreditCardData { get; set; }
        
    }
}
