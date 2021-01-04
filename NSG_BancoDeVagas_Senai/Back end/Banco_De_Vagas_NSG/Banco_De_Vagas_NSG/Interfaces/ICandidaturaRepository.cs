using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
    public interface ICandidaturaRepository
    {
        Task SeCandidatar(Candidatura NovaCandidatura);

        Task<List<Candidatura>> ListarCandidaturas(); 
    }
}
