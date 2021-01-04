using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Repositories
{
    public class CurriculoRepository : ICurriculoRepository
    {
        Context ctx = new Context();

        public async Task<Curriculo> BuscarCurriculo(int id) => await ctx.Curriculo
                                        .Include(a => a.IdCandidatoNavigation.IdUsuarioNavigation)
                                        .FirstOrDefaultAsync(a => a.IdCandidatoNavigation.IdUsuario == id);
        

        public async Task NovoCurriculo(Curriculo NovoCurriculo)
        {
			try
			{
                await ctx.Curriculo.AddAsync(NovoCurriculo);
                await ctx.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				throw ex;
			}

        }

        public async Task EditarCurriculo(int id, Curriculo CurriculoEditado)
        {
			try
			{
                Curriculo CurriculoBuscado = await ctx.Curriculo.FirstOrDefaultAsync(a => a.IdCurriculo == id);

                if (CurriculoEditado.Cursando != false)
                {
                    CurriculoBuscado.Cursando = CurriculoEditado.Cursando;
                }

                if (CurriculoEditado.Linguas != null && CurriculoEditado.Linguas != "")
                {
                    CurriculoBuscado.Linguas = CurriculoEditado.Linguas;
                }

                if (CurriculoEditado.Descricao != null && CurriculoEditado.Descricao != "")
                {
                    CurriculoBuscado.Descricao = CurriculoEditado.Descricao;
                }

                if (CurriculoEditado.Escolaridade != null && CurriculoEditado.Escolaridade != "")
                {
                    CurriculoBuscado.Escolaridade = CurriculoEditado.Escolaridade;
                }

                if (CurriculoEditado.CursosFormacoes != null && CurriculoEditado.CursosFormacoes != "")
                {
                    CurriculoBuscado.CursosFormacoes = CurriculoEditado.CursosFormacoes;
                }

                if (CurriculoEditado.PalavraChave != null && CurriculoEditado.PalavraChave != "")
                {
                    CurriculoBuscado.PalavraChave = CurriculoEditado.PalavraChave;
                }

                ctx.Curriculo.Update(CurriculoBuscado);

                await ctx.SaveChangesAsync();

            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
