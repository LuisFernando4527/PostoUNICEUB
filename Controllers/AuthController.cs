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

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
		{
			// 1. Encontra o usuário pelo e-mail
			var usuario = await _context.Usuario
				.FirstOrDefaultAsync(u => u.edEmail == loginRequest.Email);

			// 2. Verifica se o usuário existe e se a senha está correta
			// (IMPORTANTE: No mundo real, a senha deve ser "hasheada" e comparada com o hash)
			if (usuario == null || usuario.senha != loginRequest.Senha)
			{
				return Unauthorized("E-mail ou senha inválidos.");
			}

			// 3. Gera o Token JWT
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
					new Claim(ClaimTypes.Email, usuario.edEmail)
                    // Você pode adicionar mais "claims" (informações) aqui, como a role do usuário
                }),
				Expires = DateTime.UtcNow.AddHours(8), // Duração do token
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			// 4. Retorna o token para o cliente (FlutterFlow)
			return Ok(new { Token = tokenString });
		}
	}
}