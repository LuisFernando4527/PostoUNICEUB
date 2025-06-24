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

        // ==========================================================
        // ✅ NOVO ENDPOINT ADICIONADO PARA LISTAR TODOS OS ATENDIMENTOS (SEM FILTRO)
        // Este método será acessado em: /api/Atendimento
        [HttpGet]
        public async Task<IActionResult> GetTodosAtendimentos()
        {
            var resultados = await _context.Atendimento
                .Include(a => a.Paciente)
                .Include(a => a.Medico).ThenInclude(m => m.Usuario)
                .Include(a => a.Enfermeiro).ThenInclude(e => e.Usuario)
                .Select(a => new
                {
                    idAtendimento = a.idAtendimento,
                    dia = a.dtAtendimento.ToString("yyyy-MM-dd"),
                    hora = a.dtAtendimento.ToString("HH:mm"),
                    paciente = a.Paciente.nmPaciente,
                    medico = a.Medico != null ? a.Medico.Usuario.nmUsuario : "",
                    enfermeiro = a.Enfermeiro != null ? a.Enfermeiro.Usuario.nmUsuario : "",
                    status = (int)a.status
                })
                .ToListAsync();

            return Ok(resultados);
        }
        // ==========================================================


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
                    idAtendimento = a.idAtendimento,
                    dia = a.dtAtendimento.ToString("yyyy-MM-dd"),
                    hora = a.dtAtendimento.ToString("HH:mm"),
                    paciente = a.Paciente.nmPaciente,
                    // ==========================================================
                    // ✅ INÍCIO: Alteração para retornar string vazia em vez de nulo
                    medico = a.Medico != null ? a.Medico.Usuario.nmUsuario : "",
                    enfermeiro = a.Enfermeiro != null ? a.Enfermeiro.Usuario.nmUsuario : "",
                    // ✅ FIM: Alteração
                    // ==========================================================
                    status = (int)a.status
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

        [HttpGet("prontuario/{idAtendimento}")]
        public async Task<IActionResult> ObterProntuario(int idAtendimento)
        {
            var prontuario = await _context.Prontuario
                .Include(p => p.Medico)
                    .ThenInclude(m => m.Usuario)
                .Where(p => p.Atendimento.idAtendimento == idAtendimento)
                .Select(p => new
                {
                    p.idProntuario,
                    p.qp,
                    p.hda,
                    p.hpp,
                    p.exameFisico,
                    p.hd,
                    p.conduta,
                    Medico = p.Medico != null ? p.Medico.Usuario.nmUsuario : null
                })
                .FirstOrDefaultAsync();

            if (prontuario == null)
                return NotFound("Prontuário não encontrado para esse atendimento.");

            return Ok(prontuario);
        }

        [HttpGet("prescricao-medica/{idAtendimento}")]
        public async Task<IActionResult> ObterPrescricoesMedicas(int idAtendimento)
        {
            var prescricoes = await _context.PrescricaoMedica
                .Where(p => p.Atendimento.idAtendimento == idAtendimento)
                .Include(p => p.Medico)
                    .ThenInclude(m => m.Usuario)
                .Select(p => new
                {
                    p.idPrescricaoMedica,
                    p.prescricao,
                    p.horarioPrescricao,
                    Medico = p.Medico != null ? p.Medico.Usuario.nmUsuario : null
                })
                .ToListAsync();

            return Ok(prescricoes);
        }

        [HttpGet("evolucao-enfermagem/{idAtendimento}")]
        public async Task<IActionResult> ObterEvolucoesEnfermagem(int idAtendimento)
        {
            var evolucoes = await _context.EvolucaoEnfermagem
                .Where(e => e.Atendimento.idAtendimento == idAtendimento)
                .Include(e => e.Enfermeiro)
                    .ThenInclude(enf => enf.Usuario)
                .Select(e => new
                {
                    e.idEvolucaoEnfermagem,
                    data = e.dataHora.ToString("dd/MM/yyyy"),
                    hora = e.dataHora.ToString("HH:mm"),
                    e.evolucao,
                    Enfermeiro = e.Enfermeiro != null ? e.Enfermeiro.Usuario.nmUsuario : null
                })
                .ToListAsync();

            return Ok(evolucoes);
        }

        [HttpGet("estatisticas-mensais")]
        public async Task<IActionResult> ObterEstatisticasMensais()
        {
            var atendimentos = await _context.Atendimento.ToListAsync();

            var estatisticas = atendimentos
                .GroupBy(a => new { a.dtAtendimento.Year, a.dtAtendimento.Month })
                .Select(g => new
                {
                    mes = $"{g.Key.Month:D2}/{g.Key.Year}",
                    manha = g.Count(a => a.dtAtendimento.TimeOfDay < new TimeSpan(12, 0, 0)),
                    tarde = g.Count(a => a.dtAtendimento.TimeOfDay >= new TimeSpan(12, 0, 0) && a.dtAtendimento.TimeOfDay < new TimeSpan(18, 0, 0)),
                    noite = g.Count(a => a.dtAtendimento.TimeOfDay >= new TimeSpan(18, 0, 0)),
                    total = g.Count()
                })
                .OrderBy(e => e.mes)
                .ToList();

            return Ok(estatisticas);
        }
    }
}