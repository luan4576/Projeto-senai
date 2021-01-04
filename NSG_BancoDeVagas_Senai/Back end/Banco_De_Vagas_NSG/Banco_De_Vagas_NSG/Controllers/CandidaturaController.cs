using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Configurations;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Repositories;
using Banco_De_Vagas_NSG.Soluctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco_De_Vagas_NSG.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Candidato")]
    public class CandidaturaController : ControllerBase
    {
        private ICandidaturaRepository _candidaturaRepository;
        private Validations validacao;
        ICandidatoRepository _candidato;

        public CandidaturaController(ICandidaturaRepository _candidaturaRepository, Validations validacao, ICandidatoRepository _candidato)
        {
            this._candidaturaRepository = _candidaturaRepository;
            this.validacao = validacao;
            this._candidato = _candidato;
        }



        [HttpPost]
        public async Task<IActionResult> CandidatarSe([FromBody] CandidaturaUsuario NovaCandidatura)
        {
            Candidato userlogado = await _candidato.BuscarCandidato(Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value));

            bool resultado = validacao.ValidacaoCandidatura(NovaCandidatura.IdVaga, userlogado.IdCandidato, await _candidaturaRepository.ListarCandidaturas());

            if (resultado.Equals(false)) return StatusCode(403, new { msgerro = "Você já aplicou para esta vaga..." });

			try
			{
                Candidatura candidatura = new Candidatura()
                {
                    Escolhido = NovaCandidatura.Escolhido,
                    DataCandidatura = DateTime.Today,
                    IdCandidato = userlogado.IdCandidato,
                    IdVaga = NovaCandidatura.IdVaga
                };

                await _candidaturaRepository.SeCandidatar(candidatura);

                return StatusCode(200, new { msgsucesso = "Vaga aplicada com sucesso" });
            }
			catch (Exception ex)
			{
				return BadRequest(ex.InnerException);
			}
        }
    }
}
