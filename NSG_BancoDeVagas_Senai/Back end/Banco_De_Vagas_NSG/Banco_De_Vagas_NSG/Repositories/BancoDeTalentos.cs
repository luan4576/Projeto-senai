using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Repositories
{
	public class BancoDeTalentos : IBancoDeTalentos
	{
		Context ctx = new Context();
		public async Task<List<Curriculo>> BuscarCandidato(string param) =>
			await ctx.Curriculo.Include(a => a.IdCandidatoNavigation)
			.Where(a => Context.SoundsLike(a.IdCandidatoNavigation.NomeAluno) == Context.SoundsLike(param) 
			|| Context.SoundsLike(a.Descricao) == Context.SoundsLike(param) 
			|| Context.SoundsLike(a.CursosFormacoes) == Context.SoundsLike(param)
			|| Context.SoundsLike(a.PalavraChave) == Context.SoundsLike(param)
			|| Context.SoundsLike(a.Linguas) == Context.SoundsLike(param)
			|| a.Descricao.Contains(param) || a.PalavraChave.Contains(param)).ToListAsync();
	}
}
