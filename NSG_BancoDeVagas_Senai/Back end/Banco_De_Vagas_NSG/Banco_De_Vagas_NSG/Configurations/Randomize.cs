using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations
{
	public class Randomize
	{

		public string GerarSenhaAleatoria()
		{

			Random rd = new Random();
			StringBuilder builder = new StringBuilder();
			const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";


			for (int i = 0; i < 8; i++)
			{
				builder.Append(caracteres[rd.Next(caracteres.Length)]);
			}

			
			return builder.ToString();

		}
	}
}
