using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Empresa
    {
        public Empresa()
        {
            Vaga = new HashSet<Vaga>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpresa { get; set; }

        [Required]
        public string RazaoSocial { get; set; }
        
        [Required]
        public string NomeFantasia { get; set; }
        
        [Required]
        public string Cnae { get; set; }
        
        [Required]
        public string Cnpj { get; set; }

        public string ImagemEmpresa { get; set; }

        [Required]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Vaga> Vaga { get; set; }
    }
}
