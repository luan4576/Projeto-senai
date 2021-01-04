using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Banco_De_Vagas_NSG.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCurso",
                columns: table => new
                {
                    IdTipoCurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoCurso = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCurso", x => x.IdTipoCurso);
                });

            migrationBuilder.CreateTable(
                name: "TipoVaga",
                columns: table => new
                {
                    IdTipoVaga = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTipoVaga = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVaga", x => x.IdTipoVaga);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false),
                    Administrador = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    IdCandidato = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: true),
                    Cpf = table.Column<string>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.IdCandidato);
                    table.ForeignKey(
                        name: "FK_Candidato_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazaoSocial = table.Column<string>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: false),
                    Cnae = table.Column<string>(nullable: false),
                    Cnpj = table.Column<string>(nullable: false),
                    ImagemEmpresa = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.IdEmpresa);
                    table.ForeignKey(
                        name: "FK_Empresa_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curriculo",
                columns: table => new
                {
                    IdCurriculo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cursando = table.Column<bool>(nullable: false),
                    Linguas = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Escolaridade = table.Column<string>(nullable: false),
                    CursosFormacoes = table.Column<string>(nullable: false),
                    PalavraChave = table.Column<string>(nullable: false),
                    IdCandidato = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculo", x => x.IdCurriculo);
                    table.ForeignKey(
                        name: "FK_Curriculo_Candidato_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidato",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vaga",
                columns: table => new
                {
                    IdVaga = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    PalavraChave = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Salario = table.Column<int>(nullable: false),
                    StatusVaga = table.Column<bool>(nullable: false),
                    IdEmpresa = table.Column<int>(nullable: false),
                    IdTipoCurso = table.Column<int>(nullable: false),
                    IdTipoVaga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaga", x => x.IdVaga);
                    table.ForeignKey(
                        name: "FK_Vaga_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaga_TipoCurso_IdTipoCurso",
                        column: x => x.IdTipoCurso,
                        principalTable: "TipoCurso",
                        principalColumn: "IdTipoCurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vaga_TipoVaga_IdTipoVaga",
                        column: x => x.IdTipoVaga,
                        principalTable: "TipoVaga",
                        principalColumn: "IdTipoVaga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidatura",
                columns: table => new
                {
                    IdCandidatura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Escolhido = table.Column<bool>(nullable: false),
                    DataCandidatura = table.Column<DateTime>(nullable: false),
                    IdCandidato = table.Column<int>(nullable: false),
                    IdVaga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatura", x => x.IdCandidatura);
                    table.ForeignKey(
                        name: "FK_Candidatura_Candidato_IdCandidato",
                        column: x => x.IdCandidato,
                        principalTable: "Candidato",
                        principalColumn: "IdCandidato",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidatura_Vaga_IdVaga",
                        column: x => x.IdVaga,
                        principalTable: "Vaga",
                        principalColumn: "IdVaga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prazos",
                columns: table => new
                {
                    IdPrazo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrazoInicio = table.Column<DateTime>(nullable: false),
                    PrazoFim = table.Column<DateTime>(nullable: false),
                    IdVaga = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prazos", x => x.IdPrazo);
                    table.ForeignKey(
                        name: "FK_Prazos_Vaga_IdVaga",
                        column: x => x.IdVaga,
                        principalTable: "Vaga",
                        principalColumn: "IdVaga",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidato_IdUsuario",
                table: "Candidato",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatura_IdCandidato",
                table: "Candidatura",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatura_IdVaga",
                table: "Candidatura",
                column: "IdVaga");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculo_IdCandidato",
                table: "Curriculo",
                column: "IdCandidato");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_IdUsuario",
                table: "Empresa",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Prazos_IdVaga",
                table: "Prazos",
                column: "IdVaga");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_IdEmpresa",
                table: "Vaga",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_IdTipoCurso",
                table: "Vaga",
                column: "IdTipoCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Vaga_IdTipoVaga",
                table: "Vaga",
                column: "IdTipoVaga");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatura");

            migrationBuilder.DropTable(
                name: "Curriculo");

            migrationBuilder.DropTable(
                name: "Prazos");

            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropTable(
                name: "Vaga");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "TipoCurso");

            migrationBuilder.DropTable(
                name: "TipoVaga");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
