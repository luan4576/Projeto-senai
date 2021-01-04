using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Candidatura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCandidatura { get; set; }
        public bool Escolhido { get; set; }

        [Required]
        public DateTime DataCandidatura { get; set; }
        
        [Required]
        public int IdCandidato { get; set; }
        
        [Required]
        public int IdVaga { get; set; }

        [ForeignKey("IdCandidato")]
        public virtual Candidato IdCandidatoNavigation { get; set; }
        [ForeignKey("IdVaga")]
        public virtual Vaga IdVagaNavigation { get; set; }
    }
}
