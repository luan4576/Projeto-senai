using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Solutions
{
    public class UsuarioCandidato
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
        
        [Required]
        public string NomeAluno { get; set; }  

        public DateTime? DataNascimento { get; set; }

        [Required]
        public string Cpf { get; set; }
    }
}
