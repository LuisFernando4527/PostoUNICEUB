using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostoCeub.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProgressModel : PageModel
{
    private readonly PostoCeubDbContext _context;

    public ProgressModel(PostoCeubDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public bool IsReadOnly { get; set; }

    public string NomePaciente { get; set; }

    [BindProperty(SupportsGet = true)]
    public int idAtendimento { get; set; }

    [BindProperty]
    public List<EvolucaoInputModel> Evolucoes { get; set; } = new();

    public List<SelectListItem> Enfermeiros { get; set; }
    public List<SelectListItem> EnfermeirosList => Enfermeiros;

    public class EvolucaoInputModel
    {
        public DateTime DataHora { get; set; }
        public string Evolucao { get; set; }
        public int IdEnfermeiro { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var evolucoesDb = await _context.EvolucaoEnfermagem
            .Where(e => e.Atendimento.idAtendimento == idAtendimento)
            .ToListAsync();

        Evolucoes = evolucoesDb.Select(e => new EvolucaoInputModel
        {
            DataHora = e.dataHora,
            Evolucao = e.evolucao,
            IdEnfermeiro = e.idEnfermeiro ?? 0
        }).ToList();

        var atendimento = await _context.Atendimento
            .Include(a => a.Paciente)
            .FirstOrDefaultAsync(a => a.idAtendimento == idAtendimento);

        if (atendimento == null || atendimento.Paciente == null)
            return NotFound();

        NomePaciente = atendimento.Paciente.nmPaciente;

        if (Evolucoes.Count == 0 && !IsReadOnly)
            Evolucoes.Add(new EvolucaoInputModel());

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var atendimento = await _context.Atendimento.FindAsync(idAtendimento);
        if (atendimento == null)
            return NotFound();

        foreach (var item in Evolucoes)
        {
            var evolucao = new EvolucaoEnfermagem
            {
                dataHora = item.DataHora,
                evolucao = item.Evolucao,
                Enfermeiro = null,
                Atendimento = atendimento
            };

            _context.EvolucaoEnfermagem.Add(evolucao);
        }

      
        atendimento.status = StatusAtendimento.Concluido;

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Atendimento cadastrado com sucesso";

        return RedirectToPage("/Index");
    }
}
