using Store.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Digite uma senha válida")]
        [MinLength(11, ErrorMessage = "Sua senha possui menos de 11 caractéres")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Digite um CPF válido")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required]
        [MaxLength(20)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Digite um CEP válido")]
        [RegularExpression(@"^\d{5}-\d{3}", ErrorMessage = "CEP inválido")]
        public string ZipCode { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [MaxLength(50)]
        public string? Complement { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Gender { get; set; }




    }
}
