using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Banco_De_Vagas_NSG.Domains;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;

namespace Banco_De_Vagas_NSG.Contexts
{
    public partial class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SERVIDOR\\SQLEXPRESS; Initial Catalog=BancoDeVagas; Integrated Security=True");
            }
        }


        public DbSet<Candidato> Candidato { get; set; }
        public DbSet<Candidatura> Candidatura { get; set; }
        public DbSet<Curriculo> Curriculo { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Prazos> Prazos { get; set; }
        public DbSet<TipoCurso> TipoCurso { get; set; }
        public DbSet<TipoVaga> TipoVaga { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vaga> Vaga { get; set; }


        [DbFunction(Name = "SOUNDEX")]
        public static string SoundsLike(string search) => throw new NotImplementedException();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relacao in modelBuilder.Model.GetEntityTypes().SelectMany(ent => ent.GetForeignKeys()))
            {
                relacao.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.HasDbFunction(typeof(Context).GetMethod(nameof(SoundsLike)))
            .HasTranslation(args => SqlFunctionExpression.Create("SOUNDEX", args, typeof(string), null));
        }

    }
}
