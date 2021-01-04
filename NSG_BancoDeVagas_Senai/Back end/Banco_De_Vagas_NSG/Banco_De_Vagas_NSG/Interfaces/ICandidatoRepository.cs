using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
    public interface ICandidatoRepository
    {
        Task CadastrarCandidato(Candidato novoCandidato);

        Task<Candidato> BuscarCandidato(int id);

        Task EditarPerfil(int id, Candidato perfilEditado);

        Task<List<Candidatura>> MinhasCandidaturas(int id);

    }
}
