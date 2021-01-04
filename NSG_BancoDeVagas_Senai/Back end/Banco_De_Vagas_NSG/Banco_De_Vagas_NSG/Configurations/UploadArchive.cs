using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Banco_De_Vagas_NSG.Configurations
{
	public class UploadArchive 
	{
		private readonly IHostingEnvironment _ambiente;
		
		public UploadArchive(IHostingEnvironment _ambiente)
		{
			this._ambiente = _ambiente;
		}

		public async Task<string> UploadFileFolder(IFormFile imagem)
		{
			string arquivo;
			if (imagem != null && imagem.Length < 500000)
			{
				if (imagem.ContentType == "image/jpeg" || imagem.ContentType == "image/svg" || imagem.ContentType == "image/png" || imagem.ContentType == "application/octet-stream")
				{
					string diretorioImg = Path.Combine(_ambiente.ContentRootPath, "imagens");

					arquivo = "usuario"+ Guid.NewGuid() + imagem.FileName.Trim();
						
					string caminhoSalvar = Path.Combine(diretorioImg, arquivo);

					using (var stream = new FileStream(caminhoSalvar, FileMode.Create))
					{
						await imagem.CopyToAsync(stream);
					}

					return arquivo;
				}
			}

			return null;
		}
	}
}
