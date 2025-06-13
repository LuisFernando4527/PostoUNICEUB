using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using PostoUNICEUB.Services;

var builder = WebApplication.CreateBuilder(args);

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

// 🔹 Disponibiliza a role mockada como singleton
builder.Services.AddSingleton<IUserRoleService>(new MockUserRoleService(roleMock));

// 🔹 Outros serviços
builder.Services.AddDbContext<PostoCeubDbContext>(config =>
    config.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSqlServer")));

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ======================================================================
// 🚀 INÍCIO: Bloco para aplicar migrations automaticamente
// Este código executa o 'update-database' toda vez que a API inicia.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<PostoCeubDbContext>();
        dbContext.Database.Migrate(); // Aplica as migrations
    }
    catch (Exception ex)
    {
        // Loga o erro caso a migration falhe, para ajudar na depuração.
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations do banco de dados.");
    }
}
// 🚀 FIM: Bloco para aplicar migrations
// ======================================================================

// O resto do seu código continua exatamente igual
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapRazorPages();

app.Run();