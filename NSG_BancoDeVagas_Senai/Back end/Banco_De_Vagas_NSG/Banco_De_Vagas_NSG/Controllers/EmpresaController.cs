using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Configurations;
using Banco_De_Vagas_NSG.Configurations.Model;
using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Repositories;
using Banco_De_Vagas_NSG.Solutions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banco_De_Vagas_NSG.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces("application/json")]
	[Authorize(Roles = "Empresa")]
	public class EmpresaController : ControllerBase
	{

		IEmpresaRepository _empresaRepository;
		EnvioDeEmail _envioDeEmail;
		Validations validacoes;
		IVagaRepository _vagarep;
		IUsuarioRepository _usuario;
		UploadArchive _img;
		public EmpresaController(EnvioDeEmail _envioDeEmail, Validations validacoes, IVagaRepository _vagarep, IEmpresaRepository _empresaRepository, UploadArchive _img, IUsuarioRepository _usuario)
		{
			this._envioDeEmail = _envioDeEmail;
			this.validacoes = validacoes;
			this._vagarep = _vagarep;
			this._empresaRepository = _empresaRepository;
			this._img = _img;
			this._usuario = _usuario;
		}


		/// <summary>
		/// Escolha de um candidato inscrito na vaga
		/// </summary>
		/// <param name="IdCandidatura"></param>
		/// <returns>Retorna uma confirmação de que todo o processo foi bem sucedido, envia um e-mail ao candidato</returns>
		[HttpPost("EscolherCandidato/{IdCandidatura}")]
		public async Task<IActionResult> EscolherCandidato([FromRoute] int IdCandidatura)
		{
			using (Context context = new Context())
			{
				Candidatura candidatura = await context.Candidatura.AsNoTracking()
					.Include(a => a.IdVagaNavigation.IdEmpresaNavigation.IdUsuarioNavigation)
					.Include(a => a.IdCandidatoNavigation.IdUsuarioNavigation)
					.FirstOrDefaultAsync(c => c.IdCandidatura == IdCandidatura);

				if (candidatura != null && candidatura.IdVagaNavigation.StatusVaga == true)
				{
					if (candidatura.Escolhido == true) 
						return StatusCode(403, new { msgerro = $"O candidato {candidatura.IdCandidatoNavigation.NomeAluno} já foi escolhido" });

					Contact contato = new Contact()
					{
						DataCandidatura = candidatura.DataCandidatura.Year.ToString("dd/MM/yyyy"),
						Descricao = candidatura.IdVagaNavigation.Descricao,
						EmailEmpresa = candidatura.IdVagaNavigation.IdEmpresaNavigation.IdUsuarioNavigation.Email,
						TituloVaga = candidatura.IdVagaNavigation.Titulo,
						EmailUsuario = candidatura.IdCandidatoNavigation.IdUsuarioNavigation.Email,
						NomeUsuario = candidatura.IdCandidatoNavigation.NomeAluno
					};

					try
					{
						Prazos prazoEstagio = validacoes.ValidacaoEstagio(candidatura);

						if (prazoEstagio != null)
						{
							await context.Prazos.AddAsync(prazoEstagio);
							await context.SaveChangesAsync();

							AdminEstagiosAlertMail mail = new AdminEstagiosAlertMail()
							{
								DataInicioEstagio = DateTime.Today.ToString("dd/MM/yyyy"),
								DataFimEstagio = DateTime.Today.AddMonths(3).ToString("dd/MM/yyyy"),
								NomeContratado = candidatura.IdCandidatoNavigation.NomeAluno,
								NomeEmpresa = candidatura.IdVagaNavigation.IdEmpresaNavigation.NomeFantasia,
								TituloEstagio = candidatura.IdVagaNavigation.Titulo
							};

							await _vagarep.DesativarVaga(candidatura.IdVaga);
							_envioDeEmail.AdminAlertMaill(mail);
						}
							
						_vagarep.UsuarioEscolhido(candidatura.IdCandidatura);
						_envioDeEmail.CandidatoMail(contato);

					}
					catch (Exception ex)
					{
						throw ex;
					}

					return StatusCode(200, new { msgsucesso = "O candidato foi escolhido com sucesso, foi enviado um E-Mail de contato para ele" });
				}
				else
				{
					return StatusCode(404, new { msgerro = "Candidatura inválida" });
				}
			}
		}




		/// <summary>
		/// Faz o cadastro da empresa na plataforma
		/// </summary>
		/// <param name="usuarioEmpresa"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CadastroEmpresa([FromBody] UsuarioEmpresa usuarioEmpresa)
		{
			try
			{
				bool resultado = validacoes.ValidacaoEmail(_usuario.ListarUsuario(), usuarioEmpresa.Email);

				if (resultado.Equals(false)) return StatusCode(403, new { msgerro = "O E-Mail inserido já está cadastrado.." });

				resultado = validacoes.ValidacaoEmpresaCNPJ(await _empresaRepository.ListarEmpresas(), usuarioEmpresa.Cnpj);

				if (resultado.Equals(false)) return StatusCode(403, new { msgerro = "O CNPJ inserido já está cadastrado.." });


				Usuario usuario = new Usuario()
				{
					Email = usuarioEmpresa.Email,
					Senha = usuarioEmpresa.Senha,
				};

				await _usuario.CadastrarUsuario(usuario);

				Usuario novo = await this._usuario.Login(usuario.Email, usuario.Senha);

				Empresa novaEmpresa = new Empresa()
				{
					IdUsuario = novo.IdUsuario,
					RazaoSocial = usuarioEmpresa.RazaoSocial,
					NomeFantasia = usuarioEmpresa.NomeFantasia,
					Cnae = usuarioEmpresa.Cnae,
					Cnpj = usuarioEmpresa.Cnpj
				};

				await _empresaRepository.Cadastrar(novaEmpresa);

				return StatusCode(200, new { msgsucesso= "O usuário foi cadastrado com sucesso!"});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}



		/// <summary>
		/// Altera as próprias configurações
		/// </summary>
		/// <param name="id"></param>
		/// <param name="usuarioAtualizado"></param>
		/// <returns></returns>
		[HttpPut("Perfil")]
		public async Task<IActionResult> EditarPerfil([FromBody] UsuarioEmpresaAlteracao usuarioAtualizado)
		{
			int userlogado = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value);

			try
			{
				Empresa empresaEditada = new Empresa()
				{
					NomeFantasia = usuarioAtualizado.NomeFantasia,
					Cnae = usuarioAtualizado.Cnae,
					Cnpj = usuarioAtualizado.Cnpj,
					RazaoSocial = usuarioAtualizado.RazaoSocial
				};

				await _empresaRepository.Atualizar(userlogado, empresaEditada);

				return StatusCode(200, new { msgsucesso = "O usuário foi atualizado com sucesso!"});

			}
			catch (Exception erro)
			{
				return BadRequest(erro);
			}
		}


		/// <summary>
		/// Traz as informações do perfil da empresa
		/// </summary>
		/// <returns></returns>
		[HttpGet("Perfil")]
		public async Task<IActionResult> Perfil() => Ok(await _empresaRepository.BuscarPorId(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value)));





		[HttpPut("Perfil/Imagem")]
		public async Task<IActionResult> AlterarImagemEmpresa([FromForm] IFormFile file)
		{
			file = Request.Form.Files["foto"];

			try
			{
				string caminhoArquivo = await _img.UploadFileFolder(file);

				if (caminhoArquivo != null)
				{
					await _usuario.AlterarImagem(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value), caminhoArquivo);

					return StatusCode(200, new { msgsucesso = "Imagem alterada com sucesso!" });
				}

				return StatusCode(400, new { msgerro = "Ocorreu um erro na hora de atualizar, tente novamente mais tarde" });
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}






		/// <summary>
		/// Faz a listagem dos candidatos de uma vaga
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("Vagas/{id}")]
		public async Task<IActionResult> CandidatosVaga([FromRoute] int id)
		{
			Empresa idEmpresa = await _empresaRepository.BuscarPorId(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));

			try
			{
				if (validacoes.ValidacaoVagaEmpresa(await _vagarep.ListarVagas(), idEmpresa.IdEmpresa, id).Equals(false))
				{
					return Ok(await _empresaRepository.ListarCandidatosVaga(id));
				}

				return StatusCode(403, new { msgerro = "Não é permitido listar candidatos da vaga de outra empresa!" });

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}




		/// <summary>
		/// Faz a listagem de todas as vagas de uma empresa
		/// </summary>
		/// <returns></returns>
		[HttpGet("Vagas")]
		public async Task<IActionResult> VagasEmpresa() => Ok(await _vagarep.BuscarVagasEmpresa(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value)));





		/// <summary>
		/// Desativa uma vaga pelo id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPut("Vagas/Desativar/{id}")]
		public IActionResult DesativarVaga([FromRoute] int id) => Ok(_vagarep.DesativarVaga(id));






		/// <summary>
		/// Faz o cadastro de uma vaga
		/// </summary>
		/// <param name="cadastro"></param>
		/// <returns></returns>

		[HttpPost("CadastrarVaga")]
		public async Task<IActionResult> CadastrarVaga([FromBody] CadastroVaga cadastro)
		{
			Empresa empresa = await _empresaRepository.BuscarPorId(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));
			try
			{
				Vaga vaga = new Vaga()
				{
					IdEmpresa = empresa.IdEmpresa,
					Titulo = cadastro.Titulo,
					Descricao = cadastro.Descricao,
					Endereco = cadastro.Endereco,
					Salario = cadastro.Salario,
					PalavraChave = cadastro.PalavraChave,
					IdTipoCurso = cadastro.IdTipoCurso,
					IdTipoVaga = cadastro.IdTipoVaga,
					StatusVaga = cadastro.StatusVaga
				};

				await _vagarep.CadastrarVaga(vaga);

				return StatusCode(201, new { msgsucesso = "A vaga foi criada com sucesso!" });

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpGet("tiposVaga")]

		public async Task<IActionResult> BuscarTiposVaga() => Ok(await _vagarep.ListarTiposDeVaga());


		[HttpGet("tiposCurso")]

		public async Task<IActionResult> BuscarTiposCurso() => Ok(await _vagarep.ListarTiposDeCurso());



		[HttpGet("Vaga/{id}")]
		public async Task<IActionResult> VagaPeloId([FromRoute] int id) => Ok(await _vagarep.VagaPorId(id));



		[HttpPut("AlterarSenha")]
		public async Task<IActionResult> AlterarSenha([FromBody] string? senha)
		{
			Empresa empresa= await _empresaRepository.BuscarPorId(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));

			if (senha.Length > 5)
			{
				await _usuario.AlterarSenha(senha, empresa.IdUsuarioNavigation.Email);

				return StatusCode(200, new { msgsucesso = " A senha foi alterada com sucesso!" });
			}

			return StatusCode(403, new { msgerro = "A senha precisa ter no mínimo cinco caracteres.." });
		}


	}
}
