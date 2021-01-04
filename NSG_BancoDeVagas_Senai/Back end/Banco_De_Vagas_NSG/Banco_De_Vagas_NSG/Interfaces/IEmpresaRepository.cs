using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
   public interface IEmpresaRepository
    {
        Task Cadastrar(Empresa novaEmpresa);

        Task Atualizar(int id, Empresa usuarioAtualizado);

        Task<Empresa> BuscarPorId(int id);

        Task<List<Candidatura>> ListarCandidatosVaga(int id);

        Task<List<Empresa>> ListarEmpresas();
       
    }
}
