using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Pages.Treatment
{
    public class DiagnosisModel : PageModel
    {

        // Pegando ID paciente
        [BindProperty(SupportsGet = true)]
        public int idAtendimento { get; set; }

        public Atendimento Atendimento { get; set; }

        private readonly PostoCeubDbContext _context;
        public DiagnosisModel(PostoCeubDbContext context)
        {
            _context = context;
        }


        // Lista de diagnósticos disponíveis
        public List<Diagnostico> Diagnosticos { get; set; }

        // Lista de IDs selecionados
        [BindProperty]
        public List<int> SelectedDiagnosticos { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int AtendimentoId { get; set; }

        [BindProperty]
        public List<string> anotacoes { get; set; } = new();


        //Lista de Prescrições a serem observadas
        public List<string> PrescricoesPadrao { get; set; } = new List<string>
        {
            "Sinais Vitais",
            "Observar Náuseas e vômitos, palidez, tontura, suor, vistas escurecidas",
            "Observar queixas de dor, perda dos sentidos",
            "Observar local da punção venosa caso haja, realizar ECG se necessário"
        };

        public void OnGet(int idAtendimento)
        {
            Diagnosticos = _context.Diagnostico.ToList();

            // Carregar o atendimento correspondente ao idAtendimento passado
            Atendimento = _context.Atendimento
                .Include(a => a.Paciente)
                .FirstOrDefault(a => a.idAtendimento == idAtendimento);

            if (Atendimento == null)
            {
                TempData["ErrorMessage"] = "Atendimento não encontrado.";
                RedirectToPage("/Pacient");
            }
        }

        public IActionResult OnPost(int idAtendimento, List<int> diagnosticoIds)
        {
            // Buscar o atendimento
            var atendimento = _context.Atendimento.FirstOrDefault(a => a.idAtendimento == idAtendimento);

            if (atendimento == null)
            {
                return NotFound();
            }

            // Salvar os diagnósticos selecionados
            foreach (var idDiagnostico in diagnosticoIds)
            {
                var diagnosticoAtendimento = new DiagnosticoAtendimento
                {
                    idAtendimento = idAtendimento,
                    idDiagnostico = idDiagnostico
                };

                _context.DiagnosticoAtendimento.Add(diagnosticoAtendimento);
            }

            // Processar Prescrições
            foreach (var anotacao in anotacoes)
            {
                if (!string.IsNullOrWhiteSpace(anotacao))
                {
                    var prescricao = new PrescricaoEnfermagem
                    {
                        anotacao = anotacao,
                        Atendimento = _context.Atendimento.Find(idAtendimento)
                    };

                    _context.PrescricaoEnfermagem.Add(prescricao);
                }
            }

            // Salvar no banco
            _context.SaveChanges();

            // Redirect após salvar
            TempData["SuccessMessage"] = "Diagnósticos salvos com sucesso.";
            return RedirectToPage("/Treatment/Record", new { idAtendimento });
        }
    }
    }