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
	public class VagaRepository : IVagaRepository
	{
		Context ctx = new Context();

		public async Task DesativarVaga(int id)
		{
			try
			{
				var vaga = await ctx.Vaga.FirstOrDefaultAsync(a => a.IdVaga == id);
				vaga.StatusVaga = false;
				ctx.Vaga.Update(vaga);
				await ctx.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		
		}



		public async Task<Candidatura> EstagioPorId(int id) => await ctx.Candidatura
			.Include(a => a.IdCandidatoNavigation)
			.Include(a => a.IdVagaNavigation.IdEmpresaNavigation)
			.Include(a => a.IdVagaNavigation.Prazos)
			.FirstOrDefaultAsync(a => a.IdCandidatura == id);

		
		
		
		public async Task<List<Candidatura>> ListarEstagiosPrazos() => await 

			ctx.Candidatura.AsQueryable().AsNoTracking().Where(a => a.Escolhido == true && a.IdVagaNavigation.IdTipoVaga == 3)
					.Include(a => a.IdVagaNavigation.Prazos)
					.Include(a => a.IdVagaNavigation.IdTipoVagaNavigation)
					.Include(a => a.IdVagaNavigation.IdEmpresaNavigation)
					.Include(a => a.IdCandidatoNavigation).OrderByDescending(a => a.DataCandidatura)
					.ToListAsync();



		public void UsuarioEscolhido(int id)
		{
			try
			{
				Candidatura escolhido = ctx.Candidatura.FirstOrDefault(a => a.IdCandidatura == id);
				escolhido.Escolhido = true;
				ctx.Candidatura.Update(escolhido);
				ctx.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<Vaga> VagaPorId(int id) => await ctx.Vaga
			.Include(a => a.IdEmpresaNavigation)
			.Include(a => a.IdTipoVagaNavigation)
			.Include(a => a.IdTipoCursoNavigation)
			.Where(a => a.StatusVaga == true)
			.FirstOrDefaultAsync(a => a.IdVaga == id);



		public async Task<List<Vaga>> ListarVagas() => await ctx.Vaga.Include(a => a.IdEmpresaNavigation)
																.Include(a => a.IdTipoVagaNavigation)
																.Where(a => a.StatusVaga == true).ToListAsync();




		public async Task<List<Vaga>> BuscarVaga(string Titulo) => await ctx.Vaga
				.Include(a => a.IdEmpresaNavigation)
				.Include(a => a.IdTipoVagaNavigation)
				.Include(a => a.IdTipoCursoNavigation)
				.Where(a => 
				a.Titulo.Contains(Titulo) || 
				a.PalavraChave.Contains(Titulo) || 
				a.Descricao.Contains(Titulo) || 
				a.Endereco.Contains(Titulo) && a.StatusVaga == true).ToListAsync();



		public async Task CadastrarVaga(Vaga vaga)
		{
			try
			{
				await ctx.AddAsync(vaga);
				await ctx.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<List<Vaga>> BuscarVagasEmpresa(int id) => await ctx.Vaga
											.Include(a => a.IdTipoVagaNavigation)
											.Include(a => a.IdTipoCursoNavigation)
											.Include(a => a.IdEmpresaNavigation)								
											.Where(a => a.IdEmpresaNavigation.IdUsuario == id && a.StatusVaga == true)
											.ToListAsync();




		public async Task<List<Vaga>> VagaPorTipoCurso(int IdTipoCurso) => await ctx.Vaga
																	.Include(a => a.IdTipoCursoNavigation)
																	.Include(a => a.IdTipoVagaNavigation)
																	.Include(a => a.IdEmpresaNavigation).Where(a => a.IdTipoCurso == IdTipoCurso && a.StatusVaga == true).ToListAsync();


		public async Task<List<Vaga>> VagaPorTipoContrato(int IdTipoContrato) => await ctx.Vaga
																	.Include(a => a.IdTipoCursoNavigation)
																	.Include(a => a.IdTipoVagaNavigation)
																	.Include(a => a.IdEmpresaNavigation).Where(a => a.IdTipoVaga == IdTipoContrato && a.StatusVaga == true).ToListAsync();

        public async Task<List<TipoVaga>> ListarTiposDeVaga()
        {

			return await ctx.TipoVaga .AsNoTracking().ToListAsync();
				
        }

		public async Task<List<TipoCurso>> ListarTiposDeCurso()
		{

			return await ctx.TipoCurso.AsNoTracking().ToListAsync();

		}
	}
}
