using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Configurations;
using Banco_De_Vagas_NSG.Domains;
using Banco_De_Vagas_NSG.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Banco_De_Vagas_NSG.Controllers
{
    [Produces("application/json")]
	[Route("[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
        IUsuarioRepository _usuarioRepository;
        Randomize randomizacao;
        Validations validacao;
        EnvioDeEmail _envioDeEmail;

        public LoginController(IUsuarioRepository _usuarioRepository, Randomize randomizacao, Validations validacao, EnvioDeEmail _envioDeEmail)
		{
            this._usuarioRepository = _usuarioRepository;
            this.randomizacao = randomizacao;
            this.validacao = validacao;
            this._envioDeEmail = _envioDeEmail;
		}


        /// <summary>
        /// Faz a autenticação do usuário no sistema
        /// </summary>
        /// <param name="login">Objeto que conterá E-Mail e Senha para a validação e autenticação no sistema</param>
        /// <returns>Retorna um token de acesso</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginViewModel login)
        {
            Usuario usuarioBuscado = await _usuarioRepository.Login(login.Email, login.Senha);

            if (usuarioBuscado == null)
            {
                return StatusCode(404,new { msgerro = "Usuário não encontrado"});
            }

            string cargo;

            if (usuarioBuscado.Administrador == true)
            {
                cargo = "Administrador";
            }
            else if (usuarioBuscado.Candidato.Any())
            {
                cargo = "Candidato";
            }
            else
            {
                cargo = "Empresa";
            }


            var claims = new[]
            {
                new Claim("Email", usuarioBuscado.Email),
                new Claim("IdUsuario", usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, cargo),
                new Claim("role", cargo)

            };


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("BancoDeVagas-autenticacao"));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                        issuer: "Banco_De_Vagas_Back_End",
                        audience: "Banco_De_Vagas_Back_End",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
            );


            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }


        [AllowAnonymous]
        [HttpPost("ResetarSenha")]
        public async Task<IActionResult> ResetarSenhaEmail([FromBody] string email)
		{
            await Task.Delay(1000);

			if (validacao.ValidacaoEmail(_usuarioRepository.ListarUsuario(), email).Equals(false))
			{
                string novaSenha = randomizacao.GerarSenhaAleatoria();

                await _usuarioRepository.AlterarSenha(novaSenha, email);

                if(_envioDeEmail.EnviarSenhaPorEmail(novaSenha, email).Equals(false))
				{
                    return StatusCode(400, new { msgerro = "Ocorreu um erro na hora de resetar, tente resetar sua senha novamente mais tarde ou entre em contato com o administrador" });
				}
				else
				{
                    return StatusCode(200, new { msgsucesso = "Foi enviado para o seu endereço de E-Mail uma nova senha" });
				}
			}
			else
			{
                return StatusCode(200, new { msgsucesso1 = $"O endereço de E-Mail > {email} < não está cadastrado na plataforma.." });
            }


		} 

    }
}
