﻿// <auto-generated />
using System;
using Banco_De_Vagas_NSG.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Banco_De_Vagas_NSG.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Candidato", b =>
                {
                    b.Property<int>("IdCandidato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NomeAluno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCandidato");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Candidato");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Candidatura", b =>
                {
                    b.Property<int>("IdCandidatura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCandidatura")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Escolhido")
                        .HasColumnType("bit");

                    b.Property<int>("IdCandidato")
                        .HasColumnType("int");

                    b.Property<int>("IdVaga")
                        .HasColumnType("int");

                    b.HasKey("IdCandidatura");

                    b.HasIndex("IdCandidato");

                    b.HasIndex("IdVaga");

                    b.ToTable("Candidatura");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Curriculo", b =>
                {
                    b.Property<int>("IdCurriculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cursando")
                        .HasColumnType("bit");

                    b.Property<string>("CursosFormacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Escolaridade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCandidato")
                        .HasColumnType("int");

                    b.Property<string>("Linguas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PalavraChave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCurriculo");

                    b.HasIndex("IdCandidato");

                    b.ToTable("Curriculo");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Empresa", b =>
                {
                    b.Property<int>("IdEmpresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnae")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("ImagemEmpresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEmpresa");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Prazos", b =>
                {
                    b.Property<int>("IdPrazo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdVaga")
                        .HasColumnType("int");

                    b.Property<DateTime>("PrazoFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PrazoInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("IdPrazo");

                    b.HasIndex("IdVaga");

                    b.ToTable("Prazos");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.TipoCurso", b =>
                {
                    b.Property<int>("IdTipoCurso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomeTipoCurso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoCurso");

                    b.ToTable("TipoCurso");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.TipoVaga", b =>
                {
                    b.Property<int>("IdTipoVaga")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomeTipoVaga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoVaga");

                    b.ToTable("TipoVaga");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Administrador")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Vaga", b =>
                {
                    b.Property<int>("IdVaga")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoVaga")
                        .HasColumnType("int");

                    b.Property<string>("PalavraChave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salario")
                        .HasColumnType("int");

                    b.Property<bool>("StatusVaga")
                        .HasColumnType("bit");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdVaga");

                    b.HasIndex("IdEmpresa");

                    b.HasIndex("IdTipoCurso");

                    b.HasIndex("IdTipoVaga");

                    b.ToTable("Vaga");
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Candidato", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Usuario", "IdUsuarioNavigation")
                        .WithMany("Candidato")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Candidatura", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Candidato", "IdCandidatoNavigation")
                        .WithMany("Candidatura")
                        .HasForeignKey("IdCandidato")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Banco_De_Vagas_NSG.Domains.Vaga", "IdVagaNavigation")
                        .WithMany("Candidatura")
                        .HasForeignKey("IdVaga")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Curriculo", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Candidato", "IdCandidatoNavigation")
                        .WithMany("Curriculo")
                        .HasForeignKey("IdCandidato")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Empresa", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Usuario", "IdUsuarioNavigation")
                        .WithMany("Empresa")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Prazos", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Vaga", "IdVagaNavigation")
                        .WithMany("Prazos")
                        .HasForeignKey("IdVaga")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Banco_De_Vagas_NSG.Domains.Vaga", b =>
                {
                    b.HasOne("Banco_De_Vagas_NSG.Domains.Empresa", "IdEmpresaNavigation")
                        .WithMany("Vaga")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Banco_De_Vagas_NSG.Domains.TipoCurso", "IdTipoCursoNavigation")
                        .WithMany("Vaga")
                        .HasForeignKey("IdTipoCurso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Banco_De_Vagas_NSG.Domains.TipoVaga", "IdTipoVagaNavigation")
                        .WithMany("Vaga")
                        .HasForeignKey("IdTipoVaga")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
