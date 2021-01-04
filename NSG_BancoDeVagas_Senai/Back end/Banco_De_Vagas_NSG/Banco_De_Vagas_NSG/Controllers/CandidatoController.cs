using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Configurations;
using Banco_De_Vagas_NSG.Configurations.Model;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Solutions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco_De_Vagas_NSG.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class CandidatoController : ControllerBase
	{
		private UploadArchive _img;
		private IUsuarioRepository usuario;
		private ICandidatoRepository _candidatoRepository;
		private ICurriculoRepository _curriculo;
		private Validations validacao;

		public CandidatoController(UploadArchive _img, IUsuarioRepository usuario, ICandidatoRepository _candidatoRepository, ICurriculoRepository _curriculo, Validations validacao)
		{
			this._img = _img;
			this.usuario = usuario;
			this._candidatoRepository = _candidatoRepository;
			this._curriculo = _curriculo;
			this.validacao = validacao;
		}


		/// <summary>
		/// Cadastro de um candidato
		/// </summary>
		/// <param name="usuarioCandidato"></param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CadastrarCandidato([FromBody] UsuarioCandidato usuarioCandidato)
		{
			try
			{
				bool resultado = validacao.ValidacaoEmail(this.usuario.ListarUsuario(), usuarioCandidato.Email);

				if (resultado.Equals(false)) 
					return StatusCode(403, new { msgerro = "O E-Mail inserido já está cadastrado" });

				Usuario usuario = new Usuario()
				{
					Email = usuarioCandidato.Email,
					Senha = usuarioCandidato.Senha,
					Administrador = false
				};

				await this.usuario.CadastrarUsuario(usuario);

				Usuario novo = await this.usuario.Login(usuario.Email, usuario.Senha);

				Candidato candidato = new Candidato()
				{
					IdUsuario = novo.IdUsuario,
					NomeAluno = usuarioCandidato.NomeAluno,
					DataNascimento = usuarioCandidato.DataNascimento,
					Cpf = usuarioCandidato.Cpf
				};

				await _candidatoRepository.CadastrarCandidato(candidato);

				Candidato novoC = await _candidatoRepository.BuscarCandidato(novo.IdUsuario);

				Curriculo curriculo = new Curriculo()
				{
					IdCandidato = novoC.IdCandidato,
					Cursando = false,
					Descricao = "Descrição avançada sobre si, como por exemplo habilidades e experiências..",
					CursosFormacoes = "Formação acadêmica e cursos",
					Escolaridade = "A escolaridade do usuário",
					Linguas = "Adicione se tiveres alguma proeficiência num idioma específico",
					PalavraChave = "esforço, c#, aspnetcore"
				};

				await _curriculo.NovoCurriculo(curriculo);


				return StatusCode(201, new { msgsucesso = "O candidato foi cadastrado com sucesso"});

			}
			catch (Exception EX)
			{
				return BadRequest(new { msgerro = $"Ocorreu um erro, contate o administrador, {EX.Message}"});
			}
		}



		/// <summary>
		/// Alteração de foto de perfil do usuário
		/// </summary>
		/// <param name="arquivo"></param>
		/// <returns></returns>
		[Authorize(Roles = "Candidato")]
		[HttpPut("AlterarFoto")]
		public async Task<IActionResult> AlterarFoto([FromForm] IFormFile arquivo)
		{
			arquivo = Request.Form.Files["foto"];

			int userlogado = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value);

			try
			{	
				string foto = await _img.UploadFileFolder(arquivo);

				if (foto != null)
				{
					await usuario.AlterarImagem(userlogado, foto);
					return StatusCode(200, new { msgsucesso = "Imagem de perfil alterada!" });
				}

				return StatusCode(400, new { msgerro = "Ocorreu um erro na hora de atualizar, tente novamente mais tarde" });

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		/// <summary>
		/// Alterar pefil
		/// </summary>
		/// <param name="perfilEditado"></param>
		/// <returns></returns>
		[Authorize(Roles = "Candidato")]
		[HttpPut]
		public async Task<IActionResult> EditarInformacoes([FromBody] UsuarioCandidatoAlteracao perfilEditado)
		{
			int userlogado = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value);

			Candidato perfilBuscado = await _candidatoRepository.BuscarCandidato(userlogado);

			if (perfilBuscado != null)
			{
				Candidato candidatoEditado = new Candidato
				{
					NomeAluno = perfilEditado.NomeAluno,
					DataNascimento = perfilEditado.DataNascimento,
					Cpf = perfilEditado.Cpf
				};

				try
				{
					await _candidatoRepository.EditarPerfil(perfilBuscado.IdCandidato, candidatoEditado);

					return StatusCode(200, new { msgsucesso = "Editado com sucesso"});
				}
				catch (Exception erro)
				{
					return BadRequest(erro);
				}
			}

			return NotFound
				(
				new
				{
					msgerro = "Perfil não encontrado."
				}
			);
		}


		/// <summary>
		/// Faz a busca de todas as candidaturas que o candidato se aplicou
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Candidato")]
		[HttpGet("MinhasCandidaturas")]
		public async Task<IActionResult> GetMinhasCandidaturas() =>  Ok(await _candidatoRepository.MinhasCandidaturas(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value)));
		


		/// <summary>
		/// Faz a alteração do currículo
		/// </summary>
		/// <returns></returns>
		[Authorize(Roles = "Candidato")]
		[HttpPut("Curriculo")]
		public async Task<IActionResult> AtualizarCurriculo(CurriculoCandidatoAlteracao curriculoEditado)
		{
			try
			{
				int userlogado = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value);

				Curriculo editado = new Curriculo
				{
					PalavraChave = curriculoEditado.PalavraChave,
					Descricao = curriculoEditado.Descricao,
					Cursando = curriculoEditado.Cursando,
					CursosFormacoes = curriculoEditado.CursosFormacoes,
					Escolaridade = curriculoEditado.Escolaridade,
					Linguas = curriculoEditado.Linguas
				};

				Curriculo curriculo = await _curriculo.BuscarCurriculo(userlogado);

				await _curriculo.EditarCurriculo(curriculo.IdCurriculo, editado);

				return StatusCode(200, new { msgsucesso = "O seu currículo foi editado com sucesso!" });

			}
			catch (Exception)
			{

				throw;
			}
		}
		

		[Authorize(Roles = "Candidato")]
		[HttpGet("Curriculo")]
		public async Task<IActionResult> ExibirCurriculo() => Ok(await _curriculo.BuscarCurriculo(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value)));





		[Authorize(Roles = "Candidato")]
		[HttpGet("CandidaturaValida/{idVaga}")]
		public async Task<IActionResult> CandidaturaValida([FromRoute] int idVaga)
		{
			try
			{
				Candidato candidato = await _candidatoRepository.BuscarCandidato(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));

				if (validacao.CandidaturaExiste(await _candidatoRepository.MinhasCandidaturas(candidato.IdUsuario), idVaga, candidato.IdCandidato).Equals(false))
				{
					return StatusCode(200, new { msgsucesso = "Você já se candidatou á essa oportunidade" });
				}
				else
				{
					return StatusCode(200, new { msgsucesso2 = "Candidatar-se" });
				}

			}
			catch (Exception)
			{
				throw;
			}
		}


		[Authorize(Roles = "Candidato")]
		[HttpPut("AlterarSenha")]
		public async Task<IActionResult> AlterarSenha([FromBody] string? senha)
		{
			Candidato candidato =  await _candidatoRepository.BuscarCandidato(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));
			
			if(senha.Length > 5)
			{
				await usuario.AlterarSenha(senha,candidato.IdUsuarioNavigation.Email);

				return StatusCode(200, new { msgsucesso = " A senha foi alterada com sucesso!" });
			}

			return StatusCode(403, new { msgerro = "A senha precisa ter no mínimo cinco caracteres.." });
		}


	}
}
