using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
	public interface IBancoDeTalentos
	{
		Task<List<Curriculo>> BuscarCandidato(string param);
	}
}
