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
    }
}
