using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations
{
	public class Validations
	{
		public Prazos ValidacaoEstagio(Candidatura candidatura)
		{
			if (candidatura.IdVagaNavigation.IdTipoVaga == 3)
			{
				Prazos EstagioPrazo = new Prazos()
				{
					IdVaga = candidatura.IdVaga,
					PrazoInicio = DateTime.Now,
					PrazoFim = DateTime.Now.AddMonths(3)
				};

				return EstagioPrazo;
			}

			return null;
		}


		public bool ValidacaoEmail(IEnumerable<Usuario> lista, string email) 
			=> lista.Where(a => a.Email == email).Any() ? false : true;



		public bool ValidacaoCandidatura(int idVaga, int idCandidato, IEnumerable<Candidatura> Candidaturas)
			=> Candidaturas.Where(a => a.IdCandidato == idCandidato && a.IdVaga == idVaga).Any() ? false : true;


		public bool ValidacaoVagaEmpresa(IEnumerable<Vaga> vagaCandidatos, int IdEmpresa, int IdVaga)
			=> vagaCandidatos.Where(a => a.IdVaga == IdVaga && a.IdEmpresa == IdEmpresa).Any() ? false : true;


		public bool ValidacaoEmpresaCNPJ(IEnumerable<Empresa> empresa, string cnpj) 
			=> empresa.Where(a => a.Cnpj == cnpj).Any() ? false : true;


		public bool CandidaturaExiste(IEnumerable<Candidatura> candidaturas, int idVaga, int idCandidato)
			=> candidaturas.Where(a => a.IdCandidato == idCandidato && a.IdVaga == idVaga).Any() ? false : true; 
	}
}
