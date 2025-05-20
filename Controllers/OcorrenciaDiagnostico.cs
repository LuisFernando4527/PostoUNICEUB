//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//[ApiController]
//[Route("diagnosticos")]
//public class DiagnosticosController : ControllerBase
//{
//    private readonly PostoCeubDbContext _context;

//    public DiagnosticosController(PostoCeubDbContext context)
//    {
//        _context = context;
//    }

//    [HttpGet("ocorrencias")]
//    public async Task<ActionResult<IEnumerable<DiagnosticoOcorrenciaDto>>> GetOcorrencias()
//    {
//        var ocorrencias = await _context.DiagnosticoAtendimento
//            .Include(da => da.Diagnostico)
//            .GroupBy(da => da.Diagnostico.diagnostico)
//            .Select(g => new DiagnosticoOcorrenciaDto
//            {
//                Diagnostico = g.Key,
//                Quantidade = g.Count()
//            })
//            .OrderByDescending(o => o.Quantidade)
//            .ToListAsync();

//        return Ok(ocorrencias);
//    }
//}
