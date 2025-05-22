using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using System;
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

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarAtendimentos([FromQuery] string busca)
        {
            if (string.IsNullOrWhiteSpace(busca))
                return BadRequest("O parâmetro de busca é obrigatório.");

            var query = _context.Atendimento
                .Include(a => a.Paciente)
                .Include(a => a.Medico).ThenInclude(m => m.Usuario)
                .Include(a => a.Enfermeiro).ThenInclude(e => e.Usuario)
                .AsQueryable();

            // Busca por ID
            if (int.TryParse(busca, out int id))
            {
                query = query.Where(a => a.idAtendimento == id);
            }
            // Busca por data (formato: yyyy-MM-dd ou dd/MM/yyyy)
            else if (DateTime.TryParse(busca, out DateTime data))
            {
                query = query.Where(a => a.dtAtendimento.Date == data.Date);
            }
            // Busca por nome
            else
            {
                query = query.Where(a => a.Paciente.nmPaciente.Contains(busca));
            }

            var resultados = await query
                .Select(a => new
                {
                    a.idAtendimento,
                    Dia = a.dtAtendimento.ToString("yyyy-MM-dd"),
                    Hora = a.dtAtendimento.ToString("HH:mm"),
                    Paciente = a.Paciente.nmPaciente,
                    Medico = a.Medico != null ? a.Medico.Usuario.nmUsuario : null,
                    Enfermeiro = a.Enfermeiro != null ? a.Enfermeiro.Usuario.nmUsuario : null,
                })
                .ToListAsync();

            return Ok(resultados);
        }
    }
}
