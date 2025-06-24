using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PostoCeub.Data.Entities;

namespace PostoUNICEUB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public LoginModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Senha { get; set; }

        public string Erro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.edEmail == Email && u.senha == Senha);

            if (usuario == null)
            {
                Erro = "Email ou senha inválidos.";
                return Page();
            }

            // Verifica perfil (admin/medico/enfermeiro)
            var perfil = await DescobrirPerfilAsync(usuario.idUsuario);

            // Salva na sessão
            HttpContext.Session.SetInt32("UsuarioId", usuario.idUsuario);
            HttpContext.Session.SetString("Nome", usuario.nmUsuario);
            HttpContext.Session.SetString("Perfil", perfil);

            return RedirectToPage("/Index");
        }

        private async Task<string> DescobrirPerfilAsync(int idUsuario)
        {
            var isEnfermeiro = await _context.Enfermeiro.AnyAsync(e => e.idUsuario == idUsuario);

            var isMedico = await _context.Medico.AnyAsync(m => m.idUsuario == idUsuario);

            if (isEnfermeiro) return "Enfermeiro";
            if (isMedico) return "Medico";

            return "admin";
        }

    }
}
