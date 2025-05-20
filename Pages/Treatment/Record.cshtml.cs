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

        public async Task<IActionResult> OnGetAsync()
        {
            if (IsReadOnly)
            {
                // Modo de visualização - carrega prontuário existente
                Prontuario = await _context.Prontuario
                    .Include(p => p.Atendimento)
                    .FirstOrDefaultAsync(p => p.Atendimento.idAtendimento == idAtendimento);
            }
            else
            {
                // Modo de criação - inicializa com atendimento (sem carregar Prontuario do banco)
                Prontuario = new Prontuario();
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Prontuario.Atendimento");
            ModelState.Remove("Prontuario.Medico");
            // Associa o idAtendimento ao prontuário
            Prontuario.Atendimento = await _context.Atendimento.FindAsync(idAtendimento);

            if (Prontuario.Atendimento == null)
            {
                ModelState.AddModelError("", "Atendimento não encontrado.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Deu ERRO");

                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                }

                return Page();
            }

            // Salva o prontuário no banco
            _context.Prontuario.Add(Prontuario);
            await _context.SaveChangesAsync();

            // Redireciona para a página de pacientes com uma mensagem de sucesso
            return RedirectToPage("/Treatment/MedicalPrescription", new { idAtendimento = idAtendimento });

        }
    }




}
