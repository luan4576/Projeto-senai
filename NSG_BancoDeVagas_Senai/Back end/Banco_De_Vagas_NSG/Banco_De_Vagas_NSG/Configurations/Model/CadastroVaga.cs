using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations.Model
{
	public class CadastroVaga
	{
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }
        
        [Required]
        public string PalavraChave { get; set; }
        
        [Required]
        public string Endereco { get; set; }
        
        [Required]
        public int Salario { get; set; }
        
        [Required]
        public int IdTipoCurso { get; set; }
        
        [Required]
        public int IdTipoVaga { get; set; }

        
        public bool StatusVaga { get; protected set; }

		public CadastroVaga()
		{
            StatusVaga = true;
		}
    }
}
