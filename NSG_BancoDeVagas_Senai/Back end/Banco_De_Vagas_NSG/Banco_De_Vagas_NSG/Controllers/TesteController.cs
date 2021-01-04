using Banco_De_Vagas_NSG.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Banco_De_Vagas_NSG.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class TesteController : ControllerBase
	{
		Context Context = new Context();

		[HttpGet]
		public IActionResult Get() => Ok(Context.Usuario.Include(a => a.Candidato).Include(a => a.Empresa).ToList()); 
	}
}
