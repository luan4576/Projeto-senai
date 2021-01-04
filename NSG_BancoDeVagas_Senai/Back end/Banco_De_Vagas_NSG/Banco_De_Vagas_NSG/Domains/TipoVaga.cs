using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class TipoVaga
    {
        public TipoVaga()
        {
            Vaga = new HashSet<Vaga>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoVaga { get; set; }

        [Required]
        public string NomeTipoVaga { get; set; }

        public virtual ICollection<Vaga> Vaga { get; set; }
    }
}
