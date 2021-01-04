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
    public class CandidatoRepository : ICandidatoRepository
    {
        Context ctx = new Context();

        public async Task CadastrarCandidato(Candidato novoCandidato)
        {
			try
			{
                await ctx.AddAsync(novoCandidato);
                await ctx.SaveChangesAsync();
            }
			catch (Exception)
			{
				throw;
			}   
        }

        public async Task<Candidato> BuscarCandidato(int id) => await ctx.Candidato
                                                                        .Include(a => a.IdUsuarioNavigation)
                                                                        .FirstOrDefaultAsync(a => a.IdUsuario == id);
        

        public async Task EditarPerfil(int id, Candidato perfilEditado)
        {
			try
			{
                Candidato CandidatoBuscado = await ctx.Candidato.FirstOrDefaultAsync(a => a.IdCandidato == id);

                if (perfilEditado.NomeAluno != null && perfilEditado.NomeAluno != "")
                {
                    CandidatoBuscado.NomeAluno = perfilEditado.NomeAluno;
                }

                if (perfilEditado.DataNascimento != null)
                {
                    CandidatoBuscado.DataNascimento = perfilEditado.DataNascimento;
                }

                if (perfilEditado.Cpf != null && perfilEditado.Cpf != "")
                {
                    CandidatoBuscado.Cpf = perfilEditado.Cpf;
                }

                ctx.Candidato.Update(CandidatoBuscado);

                await ctx.SaveChangesAsync();

            }
			catch (Exception)
			{

				throw;
			}
        }



        public async Task<List<Candidatura>> MinhasCandidaturas(int id) => await ctx.Candidatura
                .Include(a => a.IdCandidatoNavigation)
                .Include(a => a.IdVagaNavigation)
                .Where(a => a.IdCandidatoNavigation.IdUsuario == id && a.Escolhido != true)
                .ToListAsync();
        


    }
}
