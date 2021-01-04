using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Candidato = new HashSet<Candidato>();
            Empresa = new HashSet<Empresa>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Senha { get; set; }

        [Required]
        public bool Administrador { get; set; }

        public virtual ICollection<Candidato> Candidato { get; set; }
        public virtual ICollection<Empresa> Empresa { get; set; }
    }   
}
