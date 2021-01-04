using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Prazos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPrazo { get; set; }

        [Required]
        public DateTime PrazoInicio { get; set; }

        [Required]
        public DateTime PrazoFim { get; set; }

        [Required]
        public int IdVaga { get; set; }

        [ForeignKey("IdVaga")]
        public virtual Vaga IdVagaNavigation { get; set; }
    }
}
