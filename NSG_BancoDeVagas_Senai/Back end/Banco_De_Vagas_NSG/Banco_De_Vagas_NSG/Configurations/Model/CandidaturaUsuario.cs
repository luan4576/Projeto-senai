using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Soluctions
{
    public class CandidaturaUsuario
    {
        public bool Escolhido { get; protected set; }
        public int IdVaga { get; set; }

        public CandidaturaUsuario()
        {
            Escolhido = false;
        }
    }
}
