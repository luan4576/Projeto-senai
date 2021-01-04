using Banco_De_Vagas_NSG.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ListarUsuario();

        Task CadastrarUsuario(Usuario novoUsuario);

        void AlterarUsuario(Usuario usuario);

        Task AlterarImagem(int id, string arquivonome);

        Task<Usuario> Login(string Email, string Senha);

        Task AlterarSenha(string senha, string email);

    }
}
