using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;

namespace PostoUNICEUB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PostoCeubDbContext _context;

        public IndexModel(PostoCeubDbContext context)
        {
            _context = context;
        }

        public List <Usuario> ListaUsuarios { get; set; }
        public void OnGet()
        {
           
           /* _context.Usuario.Add(new Usuario
            {
                nmUsuario = "Bruno Figueiredo",
                edEmail = "bruno.figueired@sempreceub.com",
                nuTelefone = "61 40028922"
            });
           */
            _context.SaveChanges();

             ListaUsuarios = _context.Usuario.ToList();
        }
    }
}
