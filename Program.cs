using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using PostoUNICEUB.Services; // <-- Crie essa pasta para o serviço de role

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapRazorPages();

app.Run();
