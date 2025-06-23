using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostoCeub.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PostoUNICEUB.Controllers
{
    // DTO para receber os dados de login
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    // DTO para receber os dados de cadastro
    public class RegisterRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly PostoCeubDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(PostoCeubDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ==========================================================
        // ✅ INÍCIO: Adição do endpoint de cadastro
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            // 1. Verifica se o e-mail já está em uso
            if (await _context.Usuario.AnyAsync(u => u.edEmail == registerRequest.Email))
            {
                return BadRequest("Este e-mail já está em uso.");
            }

            // 2. Cria a nova entidade Usuario
            var novoUsuario = new Usuario
            {
                nmUsuario = registerRequest.Nome,
                edEmail = registerRequest.Email,
                nuTelefone = registerRequest.Telefone,
                senha = registerRequest.Senha // IMPORTANTE: No mundo real, aqui você faria o HASH da senha
            };

            // 3. Salva o novo usuário no banco de dados
            _context.Usuario.Add(novoUsuario);
            await _context.SaveChangesAsync();

            // 4. (Opcional, mas recomendado) Loga o usuário automaticamente gerando um token
            var token = GenerateJwtToken(novoUsuario);

            return Ok(new { Token = token });
        }
        // ✅ FIM: Adição do endpoint
        // ==========================================================


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.edEmail == loginRequest.Email);

            if (usuario == null || usuario.senha != loginRequest.Senha)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = GenerateJwtToken(usuario);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("dados-protegidos")]
        public IActionResult GetDadosProtegidos()
        {
            var idUsuario = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idUsuario == null) { return Unauthorized(); }
            return Ok($"Olá, usuário com ID: {idUsuario}! Você conseguiu acessar os dados protegidos.");
        }

        // ==========================================================
        // ✅ INÍCIO: Método auxiliar para gerar o token
        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuario.edEmail)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // ✅ FIM: Método auxiliar
        // ==========================================================
    }
}