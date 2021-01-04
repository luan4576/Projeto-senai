using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations.Model
{
	public class Administrador
	{
		[DataType(DataType.EmailAddress)]
		[StringLength(30, MinimumLength = 8, ErrorMessage = "O campo precisa ter no mínimo 8 caracteres e máximo 30")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[StringLength(15, MinimumLength = 6, ErrorMessage = "O campo precisa ter no mínimo 6 caracteres e máximo 15")]
		public string Senha { get; set; }

		public virtual bool Conf { get; protected set; }

		public Administrador()
		{
			Conf = true;
		}
	}
}
