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
        [BindProperty(SupportsGet = true)]
        public bool IsReadOnly { get; set; }

        [BindProperty(SupportsGet = true)]
        public int idAtendimento { get; set; }

        public Atendimento Atendimento { get; set; }

        public string NomePaciente { get; set; }

        private readonly PostoCeubDbContext _context;

        public RecordModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Prontuario Prontuario { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var atendimento = await _context.Atendimento
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(a => a.idAtendimento == idAtendimento);

            if (atendimento == null || atendimento.Paciente == null)
                return NotFound();

            NomePaciente = atendimento.Paciente.nmPaciente;

            if (IsReadOnly)
            {
                Prontuario = await _context.Prontuario
                    .Include(p => p.Atendimento)
                    .FirstOrDefaultAsync(p => p.Atendimento.idAtendimento == idAtendimento);
            }
            else
            {
                Prontuario = new Prontuario();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Prontuario.Atendimento");
            ModelState.Remove("Prontuario.Medico");

            Prontuario.Atendimento = await _context.Atendimento.FindAsync(idAtendimento);

            if (Prontuario.Atendimento == null)
            {
                ModelState.AddModelError("", "Atendimento não encontrado.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ✅ Atualiza o status do atendimento para 2 (Prontuário)
            Prontuario.Atendimento.status = StatusAtendimento.PrescricaoMedica;

            // Salva o prontuário
            _context.Prontuario.Add(Prontuario);

            // Salva todas as alterações (incluindo o status do atendimento)
            await _context.SaveChangesAsync();

            return RedirectToPage("/Treatment/MedicalPrescription", new { idAtendimento });
        }
    }
}
