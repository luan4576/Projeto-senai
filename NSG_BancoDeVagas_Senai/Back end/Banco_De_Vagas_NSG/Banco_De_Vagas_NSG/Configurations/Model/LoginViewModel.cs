using System.ComponentModel.DataAnnotations;


namespace Banco_De_Vagas_NSG.Configurations
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}