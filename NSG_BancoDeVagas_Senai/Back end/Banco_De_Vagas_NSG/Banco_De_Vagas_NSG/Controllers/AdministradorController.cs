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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco_De_Vagas_NSG.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces("application/json")]
	[Authorize(Roles = "Administrador")]
	public class AdministradorController : ControllerBase
	{
		IVagaRepository ctx;
		IUsuarioRepository cta;
		Validations _validation;
		public AdministradorController(IVagaRepository ctx, IUsuarioRepository cta, Validations _validation )
		{
			this.ctx = ctx;
			this.cta = cta;
			this._validation = _validation;
		}

		
		/// <summary>
		/// A requisição á seguir faz a listagem de todos os estágios com os seus prazos de vencimento (ajustado para 3 meses).
		/// </summary>
		/// <returns>Retorna uma lista contendo informações de candidato, vaga, prazos e candidatura</returns>
		[HttpGet("EstagiosComPrazos")]
		public async Task<IActionResult> EstagiosComPrazos() => Ok(await ctx.ListarEstagiosPrazos());



		/// <summary>
		/// A requisição á seguir traz informações sobre um estágio em específico.
		/// </summary>
		/// <param name="id">Identificador único que trará as informações do estágio</param>
		/// <returns>Retorna um objeto com informações de candidato, vaga, prazos e candidatura</returns>
		[HttpGet("EstagiosComPrazos/{id}")]
		public async Task<IActionResult> EstagioEmDetalhe([FromRoute] int id)
		{
			var estagio = await ctx.EstagioPorId(id);

			if (estagio != null && estagio.IdVagaNavigation.Prazos.Count > 0)
			{
				return Ok(estagio);
			}

			return StatusCode(400, new { msgerro = $"A vaga de estágio id = {id} não foi encontrada." });
		}



		/// <summary>
		/// A requisição á seguir fará o cadastro de um administrador.
		/// </summary>
		/// <param name="admin"></param>
		/// <returns></returns>
		[HttpPost("CadastrarAdministrador")]
		public IActionResult CadastrarAdmin([FromBody] Administrador admin)
		{
			bool valido = _validation.ValidacaoEmail(cta.ListarUsuario(), admin.Email);

			if (valido == true)
			{
				try
				{
					Usuario usuario = new Usuario()
					{
						Administrador = admin.Conf,
						Email = admin.Email,
						Senha = admin.Senha
					};

					cta.CadastrarUsuario(usuario);

					return StatusCode(201, new { msgsucesso = "Um administrador foi cadastrado com sucesso no sistema." });
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}

			}

			return StatusCode(203, new { msgerro = "Já temos um usuário cadastrado com este E-Mail no sistema" });	
		}



		/// <summary>
		/// O administrador depois de três meses do prazo, poderá prorrogar por mais três meses ao cadastrar um novo prazo na vaga de estágio
		/// </summary>
		/// <param name="id">Id da candidatura</param>
		/// <returns></returns>
		[HttpPost("ProrrogarEstagio/{id}")]
		public async Task<IActionResult> ProrrogarEstagio([FromRoute] int id)
		{

			try
			{
				var estagio = await ctx.EstagioPorId(id);

				if (estagio == null) return StatusCode(403, new { msgerro = "Nenhum estágio foi encontrado com esse ID" });

				Prazos novoprazo = _validation.ValidacaoEstagio(estagio);

				if (novoprazo != null)
				{
					using (Context cont = new Context())
					{
						await cont.Prazos.AddAsync(novoprazo);
						await cont.SaveChangesAsync();
					}

					return StatusCode(200, new { msgsucesso = "Um novo prazo de três meses foi cadastrado com sucesso" });
				}

				return StatusCode(400, new { msgerro = "Ocorreu um erro ao tentar adicionar um prazo, tente novamente mais tarde..." });

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		

	}
}
