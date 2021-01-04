using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
    public interface ICurriculoRepository
    {
        Task NovoCurriculo(Curriculo NovoCurriculo);

        Task<Curriculo> BuscarCurriculo(int id);

        Task EditarCurriculo(int id, Curriculo CurriculoEditado);
    }
}
