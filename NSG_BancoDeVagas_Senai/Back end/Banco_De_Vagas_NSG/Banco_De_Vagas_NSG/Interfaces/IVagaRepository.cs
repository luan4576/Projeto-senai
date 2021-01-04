using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
	public interface IVagaRepository
	{
		Task DesativarVaga(int id);

		public void UsuarioEscolhido(int id);

		Task<List<Candidatura>> ListarEstagiosPrazos();

		Task<Candidatura> EstagioPorId(int id);

		Task<Vaga> VagaPorId(int id);

		Task<List<Vaga>> ListarVagas();

		Task<List<Vaga>> BuscarVaga(string titulo);

		Task CadastrarVaga(Vaga vaga);

		Task<List<Vaga>> BuscarVagasEmpresa(int id);


		Task<List<Vaga>> VagaPorTipoCurso(int IdTipoCurso);

		Task<List<Vaga>> VagaPorTipoContrato(int IdTipoContrato);

		Task<List<TipoVaga>> ListarTiposDeVaga();

		Task<List<TipoCurso>> ListarTiposDeCurso();
	}
}
