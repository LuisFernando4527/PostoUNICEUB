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

// ======================================================================
// ✅ 1. ADICIONAR SERVIÇO DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// ======================================================================

var app = builder.Build();

// ======================================================================
// ✅ 2. APLICAR MIGRATIONS AUTOMATICAMENTE
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
// ======================================================================


// Configuração do pipeline de requisições HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ======================================================================
// ✅ 3. USAR O MIDDLEWARE DO CORS
app.UseCors("AllowAll");
// ======================================================================

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapRazorPages();

app.Run();