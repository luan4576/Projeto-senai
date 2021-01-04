using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Banco_De_Vagas_NSG.Repositories
{
    public class CandidaturaRepository : ICandidaturaRepository
    {
        Context ctx = new Context();

        public async Task<List<Candidatura>> ListarCandidaturas() => await ctx.Candidatura.AsTracking().ToListAsync();

		public async Task SeCandidatar(Candidatura NovaCandidatura)
        {
			try
			{
				await ctx.Candidatura.AddAsync(NovaCandidatura);
				await ctx.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
        }


    }
}
