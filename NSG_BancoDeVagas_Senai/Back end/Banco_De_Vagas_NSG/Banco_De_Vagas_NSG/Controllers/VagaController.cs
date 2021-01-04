using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco_De_Vagas_NSG.Controllers
{
    [Produces("application/json")]

    [Route("[controller]")]
    
    [ApiController]

    [Authorize(Roles = "Candidato")]

    public class VagaController : ControllerBase
    {
        IVagaRepository _vagaRepository;

        public VagaController(IVagaRepository _vagaRepository)
        {
            this._vagaRepository = _vagaRepository;
        }


        /// <summary>
        /// Faz a listagem de todas as vagas
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _vagaRepository.ListarVagas());


        /// <summary>
        /// Faz a busca de vagas pelo título
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("pesquisa/{titulo}")]
        public async Task<IActionResult> BuscarVaga([FromRoute][FromBody] string titulo) => Ok(await _vagaRepository.BuscarVaga(titulo));





        [HttpGet("{id}")]
        public async Task<IActionResult> VagaPeloId([FromRoute][FromBody] int id) => Ok(await _vagaRepository.VagaPorId(id));



        [AllowAnonymous]
        [HttpGet("tipoCurso/{id}")]
        public async Task<IActionResult> VagaPorTipoCurso([FromRoute][FromBody] int id) => Ok(await _vagaRepository.VagaPorTipoCurso(id));


        [AllowAnonymous]
        [HttpGet("tipoContrato/{id}")]
        public async Task<IActionResult> VagaPorTipoContrato([FromRoute][FromBody] int id) => Ok(await _vagaRepository.VagaPorTipoContrato(id));
    
    
    }
}
