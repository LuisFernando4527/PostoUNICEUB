using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Pages
{
    public class PacientModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public PacientModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        public List<object> ListaPacientes { get; set; } = new List<object>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

         // Pesquisa por Pacientes
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {

                var alunos = _context.Aluno
               .Include(a => a.Paciente)
               .Where(a => a.Paciente != null && (string.IsNullOrEmpty(SearchTerm) || a.Paciente.nmPaciente.Contains(SearchTerm) || a.Paciente.idPaciente.ToString().Contains(SearchTerm)))
               .Cast<object>()
               .ToList();

            var colaboradores = _context.Colaborador
                .Include(c => c.Paciente)
                .Where(c => c.Paciente != null && (string.IsNullOrEmpty(SearchTerm) || c.Paciente.nmPaciente.Contains(SearchTerm) || c.Paciente.idPaciente.ToString().Contains(SearchTerm)))
                .Cast<object>()
                .ToList();

            var externos = _context.Paciente
                .Where(p => string.IsNullOrEmpty(SearchTerm) || p.nmPaciente.Contains(SearchTerm) || p.idPaciente.ToString().Contains(SearchTerm))
                .Where(p => !_context.Aluno.Any(a => a.Paciente.idPaciente == p.idPaciente) && !_context.Colaborador.Any(c => c.Paciente.idPaciente == p.idPaciente))
                .Cast<object>()
                .ToList();

            ListaPacientes.AddRange(alunos);
            ListaPacientes.AddRange(colaboradores);
            ListaPacientes.AddRange(externos);
            }
        }

        [BindProperty]
        public int PacienteId { get; set; } // ID do paciente selecionado para iniciar atendimento.

        // Iniciar atendimento com paciente selecionado
        public IActionResult OnPostIniciarAtendimento()
        {
            if (PacienteId > 0)
            {
                // Criar um novo atendimento
                var atendimento = new Atendimento
                {
                    idPaciente = PacienteId,
                    dtAtendimento = DateTime.Now, // Data e hora do atendimento.
                    status = StatusAtendimento.DiagnosticoDeEnfermagem
                };

                // Adicionar no banco de dados
                _context.Atendimento.Add(atendimento);
                _context.SaveChanges();

                // Redirecionar para a página de diagnóstico com o ID do Atendimento
                return RedirectToPage("/Treatment/Diagnosis", new { idAtendimento = atendimento.idAtendimento });
            }

            // Se algo der errado, exibir mensagem de erro.
            TempData["ErrorMessage"] = "Erro ao iniciar atendimento.";
            return RedirectToPage("/Pacient");
        }

    }
}
