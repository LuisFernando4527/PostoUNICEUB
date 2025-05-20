using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PostoUNICEUB.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AtendimentoController : ControllerBase
	{
		private readonly PostoCeubDbContext _context;

		public AtendimentoController(PostoCeubDbContext context)
		{
			_context = context;
		}

		[HttpGet("por-nome")]
		public async Task<IActionResult> GetAtendimentosPorNome([FromQuery] string nome)
		{
			if (string.IsNullOrWhiteSpace(nome))
				return BadRequest("O nome do paciente é obrigatório.");

			var atendimentos = await _context.Atendimento
				.Include(a => a.Paciente)
				.Include(a => a.Medico)
				.Include(a => a.Enfermeiro)
				.Where(a => a.Paciente.nmPaciente.Contains(nome))
				.Select(a => new
				{
					a.idAtendimento,
					a.dtAtendimento,
					Paciente = a.Paciente.nmPaciente,
					Medico = a.Medico != null ? a.Medico.Usuario.nmUsuario : null,
					Enfermeiro = a.Enfermeiro != null ? a.Enfermeiro.Usuario.nmUsuario : null,
				})
				.ToListAsync();

			return Ok(atendimentos);
		}
	}
}
