using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostoCeub.Data.Entities;
using System.ComponentModel.DataAnnotations;
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

    // DTO para receber os dados de ativação/criação de senha
    public class ActivateAccountRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmarSenha { get; set; }
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
        // ✅ NOVO ENDPOINT DE ATIVAÇÃO DE CONTA
        [HttpPost("activate-account")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountRequest request)
        {
            // 1. Encontra o usuário pelo e-mail
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.edEmail == request.Email);

            // 2. Verifica se o usuário realmente existe no sistema
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado. Verifique o e-mail digitado.");
            }

            // 3. Verifica se a conta já foi ativada (se a senha NÃO está vazia)
            if (!string.IsNullOrEmpty(usuario.senha))
            {
                return BadRequest("Esta conta já foi ativada e possui uma senha.");
            }

            // 4. Define a nova senha e salva no banco
            // IMPORTANTE: No mundo real, aqui você faria o HASH da senha
            usuario.senha = request.Senha;
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();

            // 5. Gera um token para logar o usuário automaticamente após a ativação
            var token = GenerateJwtToken(usuario);
            return Ok(new { Token = token });
        }
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
    }
}