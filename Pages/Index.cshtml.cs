using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public IndexModel(PostoCeubDbContext context)
        {
            _context = context;
        }


        public List<Atendimento> ListaAtendimentos { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            // Inclui os dados do Paciente ao buscar os Atendimentos
            var query = _context.Atendimento
                 .Include(a => a.Paciente)  // Inclui o paciente relacionado a cada atendimento
                 .AsQueryable();

            // Filtra por nome do paciente, ID do atendimento ou data de atendimento
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(a => a.Paciente.nmPaciente.Contains(SearchTerm)
                                      || a.idAtendimento.ToString().Contains(SearchTerm)
                                      || a.dtAtendimento.ToString().Contains(SearchTerm));
            }

            ListaAtendimentos = query.ToList();
        }
    }
}
