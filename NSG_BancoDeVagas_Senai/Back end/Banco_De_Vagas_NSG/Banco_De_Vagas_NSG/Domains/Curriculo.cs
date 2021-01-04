using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Curriculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCurriculo { get; set; }
        
        [Required]
        public bool Cursando { get; set; }
        
        [Required]
        public string Linguas { get; set; }

        [Required]
        //[DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required]
        public string Escolaridade { get; set; }
        
        [Required]
        public string CursosFormacoes { get; set; }
        
        [Required]
        public string PalavraChave { get; set; }
        
        [Required]
        public int IdCandidato { get; set; }

        [ForeignKey("IdCandidato")]
        public virtual Candidato IdCandidatoNavigation { get; set; }
    }
}
