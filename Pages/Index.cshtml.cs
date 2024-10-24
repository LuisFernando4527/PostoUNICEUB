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

        public void OnGet()
        {
            // Inclui os dados do Paciente ao buscar os Atendimentos
            ListaAtendimentos = _context.Atendimento
                .Include(a => a.Paciente)  // Inclui o paciente relacionado a cada atendimento
                .ToList();
        }
    }
}
