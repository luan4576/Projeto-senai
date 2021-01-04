using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco_De_Vagas_NSG.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class BancoDeTalentosController : ControllerBase
	{
		IBancoDeTalentos _bancoDeTalentos;
		ICurriculoRepository _candidato;
		public BancoDeTalentosController(IBancoDeTalentos _bancoDeTalentos, ICurriculoRepository _candidato)
		{
			this._bancoDeTalentos = _bancoDeTalentos;
			this._candidato = _candidato;
		}


		/// <summary>
		/// Faz a busca pelos currículos que estão cadastrados no site
		/// </summary>
		/// <param name="busca"></param>
		/// <returns></returns>
		[HttpGet("{busca}")]
		public async Task<IActionResult> BuscarTalento(string busca) => Ok(await _bancoDeTalentos.BuscarCandidato(busca));


		/// <summary>
		/// Faz a busca do currículo do candidato
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("curriculo/{id}")]
		public async Task<IActionResult> CurriculoPorId([FromRoute] int id) => Ok(await _candidato.BuscarCurriculo(id));

	}
}
