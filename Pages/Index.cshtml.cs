using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using PostoUNICEUB.Models;
using PostoUNICEUB.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostoUNICEUB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserRoleService _roleService;
        private readonly PostoCeubDbContext _context;

        public IndexModel(IUserRoleService roleService, PostoCeubDbContext context)
        {
            _roleService = roleService;
            _context = context;
        }

        public bool IsMedico => _roleService.IsMedico();
        public bool IsEnfermeiro => _roleService.IsEnfermeiro();
        public bool IsAdmin => _roleService.IsAdmin();

        public readonly Dictionary<int, string> StatusNomes = new()
        {
            {1, "Diagnóstico de Enfermagem"},
            {2, "Prontuário"},
            {3, "Prescrição Médica"},
            {4, "Evolução"},
            {5, "Concluído"}
        };

        public List<AtendimentoCardModel> AtendimentosPreenchiveis { get; set; } = new();
        public List<AtendimentoCardModel> AtendimentosNaoPreenchiveis { get; set; } = new();
        public List<AtendimentoCardModel> TodosAtendimentosAdmin { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        //Objetos Páginação
        public const int PageSize = 6;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }
        public int TotalAtendimentosNaoPreenchiveis { get; set; }

        public List<AtendimentoCardModel> AtendimentosNaoPreenchiveisPaginados { get; set; } = new();


        public async Task OnGetAsync()
        {
            var lista = await ObterTodosAtendimentosOrdenadosPorDataDesc();

            foreach (var atendimento in lista)
            {
                var status = atendimento.status;
                var statusInt = (int)status;
                var statusNome = StatusNomes.ContainsKey(statusInt) ? StatusNomes[statusInt] : "Desconhecido";

                var card = new AtendimentoCardModel
                {
                    Atendimento = atendimento,
                    StatusNome = statusNome,
                    StatusClass = statusInt == 5 ? "status-concluido" : "status-alerta",
                    StatusBtn = statusInt == 5 ? "check-status" : "warning-status"
                };

                if (IsAdmin)
                {
                    card.MostrarPreencher = true;
                    card.LinkPreencher = ObterLinkPorStatus(status);
                    card.PodeEditar = true;
                    TodosAtendimentosAdmin.Add(card);
                }
                else if (IsMedico)
                {
                    if (status == StatusAtendimento.Prontuario || status == StatusAtendimento.PrescricaoMedica)
                    {
                        card.MostrarPreencher = true;
                        card.LinkPreencher = ObterLinkPorStatus(status);
                        AtendimentosPreenchiveis.Add(card);
                    }
                    else
                    {
                        AtendimentosNaoPreenchiveis.Add(card);
                    }
                }
                else if (IsEnfermeiro)
                {
                    if (status == StatusAtendimento.DiagnosticoDeEnfermagem || status == StatusAtendimento.Evolucao)
                    {
                        card.MostrarPreencher = true;
                        card.LinkPreencher = ObterLinkPorStatus(status);
                        AtendimentosPreenchiveis.Add(card);
                    }
                    else
                    {
                        AtendimentosNaoPreenchiveis.Add(card);
                    }
                }
            }
            if (IsMedico || IsEnfermeiro)
            {
                TotalAtendimentosNaoPreenchiveis = AtendimentosNaoPreenchiveis.Count;
                TotalPages = (int)Math.Ceiling(TotalAtendimentosNaoPreenchiveis / (double)PageSize);

                AtendimentosNaoPreenchiveisPaginados = AtendimentosNaoPreenchiveis
                    .Skip((PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }

        }

        private async Task<List<Atendimento>> ObterTodosAtendimentosOrdenadosPorDataDesc()
        {
            var query = _context.Atendimento
                .Include(a => a.Paciente)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(a =>
                    a.Paciente.nmPaciente.Contains(SearchTerm) ||
                    a.idAtendimento.ToString().Contains(SearchTerm) ||
                    a.dtAtendimento.ToString().Contains(SearchTerm));
            }

            return await query.OrderByDescending(a => a.dtAtendimento).ToListAsync();
        }

        private string? ObterLinkPorStatus(StatusAtendimento status)
        {
            return status switch
            {
                StatusAtendimento.DiagnosticoDeEnfermagem => "/Treatment/Diagnosis",
                StatusAtendimento.Evolucao => "/Treatment/Progress",
                StatusAtendimento.Prontuario => "/Treatment/Record",
                StatusAtendimento.PrescricaoMedica => "/Treatment/Prescription",
                _ => null
            };
        }
    }
}
