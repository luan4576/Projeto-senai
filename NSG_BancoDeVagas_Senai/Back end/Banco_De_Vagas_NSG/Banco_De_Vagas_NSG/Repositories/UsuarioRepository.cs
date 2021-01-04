using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Banco_De_Vagas_NSG.Repositories
{


    public class UsuarioRepository : IUsuarioRepository
    {
        Context ctx = new Context();

        private string stringConexao = "Data Source=SERVIDOR\\SQLEXPRESS; Initial Catalog=BancoDeVagas; Integrated Security=True";

		public async Task AlterarImagem(int id, string arquivonome)
		{
			try
			{
                var usuario = await ctx.Usuario
               .Include(a => a.Candidato)
               .Include(a => a.Empresa)
               .FirstOrDefaultAsync(a => a.IdUsuario == id);

                if (usuario.Candidato.Any())
                {
                    var resp = await ctx.Candidato.FirstOrDefaultAsync(a => a.IdUsuario == usuario.IdUsuario);
                    resp.Foto = arquivonome;
                    ctx.Candidato.Update(resp);
                    await ctx.SaveChangesAsync();
                }

				if (usuario.Empresa.Any())
				{
                    var resp = await ctx.Empresa.FirstOrDefaultAsync(a => a.IdUsuario == usuario.IdUsuario);
                    resp.ImagemEmpresa = arquivonome;
                    ctx.Empresa.Update(resp);
                    await ctx.SaveChangesAsync();
                }

            }
			catch (Exception ex)
			{
				throw ex;
			}
           
        }

		public async Task AlterarSenha(string senha, string email)
		{
			try
			{
                if (senha != "" || senha != null)
                {
                    Usuario usr = await ctx.Usuario.FirstOrDefaultAsync(a => a.Email == email);

                    usr.Senha = senha;

                    ctx.Usuario.Update(usr);

                    await ctx.SaveChangesAsync();
                }
            }
			catch (Exception)
			{

				throw;
			}
            
		}

		public void AlterarUsuario(Usuario usuario)
        {
            string Query = "update Usuario set Email = @Email, Senha = @Senha where IdUsuario = @IdUsuario";
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public async Task CadastrarUsuario(Usuario novoUsuario)
        {
            string Query = "insert into Usuario (Email, Senha, Administrador) values(@Email, @Senha, @Administrador)";

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Email", novoUsuario.Email);
                cmd.Parameters.AddWithValue("@Senha", novoUsuario.Senha);
                cmd.Parameters.AddWithValue("@Administrador", novoUsuario.Administrador);

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        
        public List<Usuario> ListarUsuario() => ctx.Usuario.AsTracking().ToList();

        
        
        public async Task<Usuario> Login(string Email, string Senha) => await 
            ctx.Usuario.AsTracking()
            .Include(a => a.Candidato)
            .Include(a => a.Empresa).Where(a => a.Email == Email && a.Senha == Senha)
            .FirstOrDefaultAsync();



    }
}
