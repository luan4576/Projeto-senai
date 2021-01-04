using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Solutions
{
    public class UsuarioEmpresa
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string RazaoSocial { get; set; }
        [Required]
        public string NomeFantasia { get; set; }
        [Required]
        public string Cnae { get; set; }
        [Required]
        public string Cnpj { get; set; }
    }
}
