
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

            if (int.TryParse(busca, out int id))
            {
                query = query.Where(a => a.idAtendimento == id);
            }
            else if (DateTime.TryParse(busca, out DateTime data))
            {
                query = query.Where(a => a.dtAtendimento.Date == data.Date);
            }
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

        [HttpGet("resumo/{idAtendimento}")]
        public async Task<IActionResult> ObterResumo(int idAtendimento)
        {
            var prescricoesEnfermagem = await _context.PrescricaoEnfermagem
                .Where(p => p.idAtendimento == idAtendimento)
                .Include(p => p.Enfermeiro)
                    .ThenInclude(e => e.Usuario)
                .Select(p => new
                {
                    p.idPrescricaoEnfermagem,
                    p.anotacao,
                    Enfermeiro = p.Enfermeiro != null ? p.Enfermeiro.Usuario.nmUsuario : null
                })
                .ToListAsync();

            var diagnosticoIds = await _context.DiagnosticoAtendimento
                .Where(d => d.idAtendimento == idAtendimento)
                .Select(d => d.idDiagnostico)
                .ToListAsync();

            var diagnosticos = await _context.Diagnostico
                .Where(d => diagnosticoIds.Contains(d.idDiagnostico))
                .ToListAsync();

            var resultado = new
            {
                prescricoesEnfermagem,
                diagnostico = diagnosticos
            };

            return Ok(resultado);
        }

    }
}
