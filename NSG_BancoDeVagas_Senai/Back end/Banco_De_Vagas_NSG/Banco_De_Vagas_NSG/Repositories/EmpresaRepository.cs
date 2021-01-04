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
    public class EmpresaRepository :IEmpresaRepository
    {
        Context ctx = new Context();

        public async Task Atualizar(int id, Empresa usuarioAtualizado)
        {
			try
			{
                Empresa empresaBuscada = await ctx.Empresa.FirstOrDefaultAsync(a => a.IdUsuario == id);

				if (usuarioAtualizado.RazaoSocial != null && usuarioAtualizado.RazaoSocial != "")
				{
                    empresaBuscada.RazaoSocial = usuarioAtualizado.RazaoSocial;
                }
				if (usuarioAtualizado.NomeFantasia != null && usuarioAtualizado.NomeFantasia != "")
				{
                    empresaBuscada.NomeFantasia = usuarioAtualizado.NomeFantasia;
                }
                if (usuarioAtualizado.Cnae != null && usuarioAtualizado.Cnae != "")
                {
                    empresaBuscada.Cnae = usuarioAtualizado.Cnae;
                }
                if (usuarioAtualizado.Cnpj != null && usuarioAtualizado.Cnpj != "")
                {
                    empresaBuscada.Cnpj = usuarioAtualizado.Cnpj;
                }

                ctx.Empresa.Update(empresaBuscada);
                await ctx.SaveChangesAsync();

            }
			catch (Exception ex)
			{
				throw ex;
			}
        }

        public async Task<Empresa> BuscarPorId(int id) => await ctx.Empresa.Include(a => a.IdUsuarioNavigation).FirstOrDefaultAsync(u => u.IdUsuario == id);
         

        public async Task Cadastrar(Empresa novaEmpresa)
        {
			try
			{
                await ctx.Empresa.AddAsync(novaEmpresa);
                await ctx.SaveChangesAsync();
            }
			catch (Exception ex)
			{
				throw ex;
			}

        }

        public async Task<List<Candidatura>> ListarCandidatosVaga(int id) => await ctx.Candidatura.Include(a => a.IdCandidatoNavigation)
                                                                                .Include(a => a.IdVagaNavigation).Include(a => a.IdCandidatoNavigation)
                                                                                .Where(a => a.IdVaga == id).ToListAsync();


        public Task<List<Empresa>> ListarEmpresas() => ctx.Empresa.AsTracking().ToListAsync();

	}
}
