using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Candidato
    {
        public Candidato()
        {
            Candidatura = new HashSet<Candidatura>();
            Curriculo = new HashSet<Curriculo>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCandidato { get; set; }

        [Required]
        public string NomeAluno { get; set; }
        
        public string? Foto { get; set; }
        
        public DateTime? DataNascimento { get; set; }
        
        [Required]
        public string Cpf { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Candidatura> Candidatura { get; set; }
        public virtual ICollection<Curriculo> Curriculo { get; set; }
    }
}
