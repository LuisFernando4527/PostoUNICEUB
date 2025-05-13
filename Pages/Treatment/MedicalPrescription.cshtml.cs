using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostoUNICEUB.Pages.Treatment
{
    public class MedicalPrescriptionModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public MedicalPrescriptionModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int idAtendimento { get; set; }

        public string NomePaciente { get; set; }

        [BindProperty]
        public List<PrescricaoInputModel> Prescricoes { get; set; } = new();

        public class PrescricaoInputModel
        {
            public string Prescricao { get; set; }
            public string HorarioPrescricao { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var atendimento = await _context.Atendimento
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(a => a.idAtendimento == idAtendimento);

            if (atendimento == null || atendimento.Paciente == null)
                return NotFound();

            NomePaciente = atendimento.Paciente.nmPaciente;

            // Adiciona um item inicial
            Prescricoes.Add(new PrescricaoInputModel());

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var atendimento = await _context.Atendimento.FindAsync(idAtendimento);
            if (atendimento == null)
                return NotFound();

            foreach (var item in Prescricoes)
            {
                var prescricao = new PrescricaoMedica
                {
                    prescricao = item.Prescricao,
                    horarioPrescricao = item.HorarioPrescricao,
                    Atendimento = atendimento
                };

                _context.PrescricaoMedica.Add(prescricao);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Treatment/MedicalPrescription", new { idAtendimento });
        }
    }
}
