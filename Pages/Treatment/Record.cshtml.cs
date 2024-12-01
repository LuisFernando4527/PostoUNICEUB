using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Pages.Treatment
{
    public class RecordModel : PageModel
    {

        // Pegando ID paciente
        [BindProperty(SupportsGet = true)]
        public int idAtendimento { get; set; }

        public Atendimento Atendimento { get; set; }

        private readonly PostoCeubDbContext _context;
        public RecordModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prontuario Prontuario { get; set; }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Associa o idAtendimento ao prontuário
            Prontuario.Atendimento = await _context.Atendimento.FindAsync(idAtendimento);

            if (Prontuario.Atendimento == null)
            {
                ModelState.AddModelError("", "Atendimento não encontrado.");
                return Page();
            }

            // Salva o prontuário no banco
            _context.Prontuario.Add(Prontuario);
            await _context.SaveChangesAsync();

            // Redireciona para a página de pacientes com uma mensagem de sucesso
            return RedirectToPage("/Pacient", new { success = true });
        }
    }




}
