using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations.Model
{
	public class CurriculoCandidatoAlteracao
	{
        [Required]
        public bool Cursando { get; set; }

        public string? Linguas { get; set; }

        public string? Descricao { get; set; }

        public string? Escolaridade { get; set; }

        public string? CursosFormacoes { get; set; }

        public string? PalavraChave { get; set; }

    }
}
