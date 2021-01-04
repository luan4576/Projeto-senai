using Banco_De_Vagas_NSG.Configurations.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations
{
	public class EnvioDeEmail
	{
		IConfiguration _configuration;
		SmtpClient _smtp;
		

		public EnvioDeEmail(SmtpClient _smtp, IConfiguration _configuration)
		{
			this._smtp = _smtp;
			this._configuration = _configuration;
		}

		public void AdminAlertMaill(AdminEstagiosAlertMail mail)
		{
			string msg = string.Format($"<p>O candidato {mail.NomeContratado} escolhido pela empresa " +
				$"{mail.NomeEmpresa} para estagiar como {mail.TituloEstagio}...</p> \n \n <p><b> Esta é uma mensagem automática, não responda de volta, reenvie ou encaminhe para ninguém. " +
				$"SENAI SÃO PAULO - GRUPO NSG.</b><p>");

			MailMessage mensagem = new MailMessage();
			mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
			mensagem.To.Add("insira o email do adm");
			mensagem.Subject = ($"Banco de Vagas do SENAI - Notificação - Estágio ativado - " + DateTime.Today.ToString("dd/MM/yyyy"));
			mensagem.Body = msg;
			mensagem.IsBodyHtml = true;
			_smtp.Send(mensagem);
		}

		public void CandidatoMail(Contact contact)
		{

			string msg = string.Format(
					$"<center><h2>Banco de vagas do SENAI - Notificação </h2></center> \n \n"  +
					$"<h2>Você foi selecionado no processo seletivo para  vaga de {contact.TituloVaga}</h2> \n \n" +
					$"<p> Email da empresa: {contact.EmailEmpresa} <p> \n" +
					$"<p> Título da vaga: {contact.TituloVaga} </p> \n" +
					$"<p> Descrição da vaga: {contact.Descricao} </p> \n" +
					$"<p> Data da candidatura: {contact.DataCandidatura} </p> \n" +
					$"<p> Entre em contato com a empresa pelo E-Mail para mais detalhes </p> \n" +
					$"<p> <b>Este é um E-Mail automático, não responda, reenvie ou encaminhe para ninguém!</b> </p> \n" +
					$"<p> <b>SENAI SÃO PAULO DE INFORMÁTICA - GRUPO NSG - 2020</b> </p>");

			MailMessage mensagem = new MailMessage();
			mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
			mensagem.To.Add(contact.EmailUsuario);
			mensagem.Subject = ($"Banco de Vagas do SENAI - Notificação " + DateTime.Today.ToString("dd/MM/yyyy"));
			mensagem.Body = msg;
			mensagem.IsBodyHtml = true;
			_smtp.Send(mensagem);
		}


		public bool EnviarSenhaPorEmail(string senha, string email)
		{
			string mensagem = string.Format(
				"<h3> A sua senha de usuário foi resetada com sucesso na plataforma Banco de vagas </h3>\n" +
				"<p>Seu novo login é:</p> \n" +
				$"<p>Email: {email} </p>\n" +
				$"<p>Senha: {senha} </p>\n\n" +
				"<p>Este é um E-Mail automático, não responda de volta. SENAI SÃO PAULO, Banco de Vagas - Grupo NSG.</p>"
				);

			MailMessage msgMail = new MailMessage();

			msgMail.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
			msgMail.To.Add(email);
			msgMail.Subject = string.Format($"Banco de Vagas do SENAI - Notificação - Senha resetada! -" + DateTime.Today.ToString("dd/MM/yyyy"));
			msgMail.Body = mensagem;
			msgMail.IsBodyHtml = true;

			try
			{
				_smtp.Send(msgMail);
				return true;
			}
			catch (SmtpFailedRecipientException)
			{
				return false;
				throw;
			}

		}

	}
}
