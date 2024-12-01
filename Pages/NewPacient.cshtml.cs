using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PostoUNICEUB.Pages
{
    public class NewPacientModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public NewPacientModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Paciente Paciente { get; set; }

        [BindProperty]
        public Aluno Aluno { get; set; }

        [BindProperty]
        public Colaborador Colaborador { get; set; }

        

        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica o tipo de paciente
            string pacienteType = Request.Form["pacienteType"];
            if (string.IsNullOrEmpty(pacienteType))
            {
                ModelState.AddModelError(string.Empty, "O tipo de paciente não foi selecionado.");
                return Page(); // Retorna à página com a mensagem de erro
            }

            // Limpa o ModelState para validação personalizada
            ModelState.Clear();

            


            // Valida campos relevantes para Aluno
            if (pacienteType == "aluno")
            {
                if (string.IsNullOrWhiteSpace(Aluno.ra))
                {
                    ModelState.AddModelError(nameof(Aluno.ra), "O campo RA é obrigatório.");
                }
            }
            // Valida campos relevantes para Colaborador
            else if (pacienteType == "colaborador")
            {
                if (string.IsNullOrWhiteSpace(Colaborador.matricula))
                {
                    ModelState.AddModelError(nameof(Colaborador.matricula), "O campo Matrícula é obrigatório.");
                }
            }

            // Se o ModelState não é válido, retorne à página com erros
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page(); // Retorna à página com as mensagens de erro
            }


            var existingPaciente = await _context.Paciente.FirstOrDefaultAsync(p => p.nuCPF == Paciente.nuCPF);

            if (existingPaciente != null)
            {
                ModelState.AddModelError("Paciente.nuCPF", "CPF já cadastrado.");
                return Page(); // Retorna à página com a mensagem de erro
            }

            // Salva o Paciente
            _context.Paciente.Add(Paciente);
            await _context.SaveChangesAsync(); // Salva o paciente primeiro

            // Atribui o Paciente ao Aluno ou ao Colaborador conforme o tipo
            if (pacienteType == "aluno")
            {
                Aluno.Paciente = Paciente; // Define a relação entre Aluno e Paciente
                _context.Aluno.Add(Aluno);
            }
            else if (pacienteType == "colaborador")
            {
                Colaborador.Paciente = Paciente; // Define a relação entre Colaborador e Paciente
                _context.Colaborador.Add(Colaborador);
            }

            // Salva as alterações no banco de dados
            await _context.SaveChangesAsync();

            return RedirectToPage("/Pacient", new { success = true });
        }



    }
}
