using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PostoUNICEUB.Pages.Treatment
{
    public class DiagnosisModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public DiagnosisModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public bool IsReadOnly { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public int idAtendimento { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool readonlyMode { get; set; }

        public Atendimento Atendimento { get; set; }

        public List<Diagnostico> Diagnosticos { get; set; } = new();
        public List<int> DiagnosticosMarcados { get; set; } = new();

        public List<string> PrescricoesPadrao { get; set; } = new()
        {
            "Sinais Vitais",
            "Observar Náuseas e vômitos, palidez, tontura, suor, vistas escurecidas",
            "Observar queixas de dor, perda dos sentidos",
            "Observar local da punção venosa caso haja, realizar ECG se necessário"
        };

        public List<PrescricaoEnfermagem> PrescricoesExistentes { get; set; } = new();

        [BindProperty]
        public List<int> SelectedDiagnosticos { get; set; } = new();

        [BindProperty]
        public List<string> anotacoes { get; set; } = new();

        public void OnGet()
        {
            PrescricoesExistentes = _context.PrescricaoEnfermagem
                .Where(p => p.Atendimento.idAtendimento == idAtendimento)
                .ToList();

            Diagnosticos = _context.Diagnostico.ToList();

            Atendimento = _context.Atendimento
                .Include(a => a.Paciente)
                .FirstOrDefault(a => a.idAtendimento == idAtendimento);

            if (Atendimento == null)
            {
                TempData["ErrorMessage"] = "Atendimento não encontrado.";
                RedirectToPage("/Pacient");
            }

            DiagnosticosMarcados = _context.DiagnosticoAtendimento
                .Where(da => da.idAtendimento == idAtendimento)
                .Select(da => da.idDiagnostico)
                .ToList();
        }

        public IActionResult OnPost()
        {
            var atendimento = _context.Atendimento.FirstOrDefault(a => a.idAtendimento == idAtendimento);
            if (atendimento == null)
            {
                return NotFound();
            }

            // Atualiza status do atendimento para Diagnóstico de Enfermagem
            atendimento.status = StatusAtendimento.Prontuario;

            foreach (var idDiagnostico in SelectedDiagnosticos)
            {
                var diagnosticoAtendimento = new DiagnosticoAtendimento
                {
                    idAtendimento = idAtendimento,
                    idDiagnostico = idDiagnostico
                };
                _context.DiagnosticoAtendimento.Add(diagnosticoAtendimento);
            }

            foreach (var anotacao in anotacoes)
            {
                if (!string.IsNullOrWhiteSpace(anotacao))
                {
                    var prescricao = new PrescricaoEnfermagem
                    {
                        anotacao = anotacao,
                        Atendimento = atendimento
                    };
                    _context.PrescricaoEnfermagem.Add(prescricao);
                }
            }

            _context.SaveChanges();

            TempData["SuccessMessage"] = "Diagnósticos salvos com sucesso.";
            return RedirectToPage("/Treatment/Record", new { idAtendimento });
        }
    }
}
