using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostoCeub.Data.Entities;
using PostoUNICEUB.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ======================================================================
// ✅ 1. CONFIGURAÇÃO DE AUTENTICAÇÃO JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("Jwt:Key", "A chave JWT não pode ser nula no appsettings.json");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Em produção, considere usar true
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true
    };
});
// ======================================================================

// 🔹 Registrar o valor da role (mock) passado por linha de comando
string roleMock = "admin"; // padrão
if (args.Length > 0)
{
    var roleArg = args[0].ToLower();
    var rolesValidos = new[] { "medico", "enfermeiro", "admin" };
    if (rolesValidos.Contains(roleArg))
    {
        roleMock = roleArg;
    }
}
builder.Services.AddSingleton<IUserRoleService>(new MockUserRoleService(roleMock));

// 🔹 Outros serviços
builder.Services.AddDbContext<PostoCeubDbContext>(config =>
    config.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSqlServer")));

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar serviço de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<PostoCeubDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations do banco de dados.");
    }
}

// Configuração do pipeline de requisições HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

// ======================================================================
// ✅ 2. USAR O MIDDLEWARE DE AUTENTICAÇÃO
// A ordem é crucial: Autenticação vem ANTES de Autorização.
app.UseAuthentication();
// ======================================================================

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapRazorPages();

app.Run();