using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class TipoCurso
    {
        public TipoCurso()
        {
            Vaga = new HashSet<Vaga>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoCurso { get; set; }

        [Required]
        public string NomeTipoCurso { get; set; }

        public virtual ICollection<Vaga> Vaga { get; set; }
    }
}
