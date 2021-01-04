using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco_De_Vagas_NSG.Domains
{
    public partial class Vaga
    {
        public Vaga()
        {
            Candidatura = new HashSet<Candidatura>();
            Prazos = new HashSet<Prazos>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVaga { get; set; }
        
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
        public bool StatusVaga { get; set; }
        
        [Required]
        public int IdEmpresa { get; set; }

        [Required]
        public int IdTipoCurso { get; set; }
        
        [Required]
        public int IdTipoVaga { get; set; }


        [ForeignKey("IdEmpresa")]
        public virtual Empresa IdEmpresaNavigation { get; set; }
        
        [ForeignKey("IdTipoCurso")]
        public virtual TipoCurso IdTipoCursoNavigation { get; set; }

        [ForeignKey("IdTipoVaga")]
        public virtual TipoVaga IdTipoVagaNavigation { get; set; }

        public virtual ICollection<Candidatura> Candidatura { get; set; }
        public virtual ICollection<Prazos> Prazos { get; set; }
    }
}
