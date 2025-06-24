using Microsoft.EntityFrameworkCore;
using PostoCeub.Data.Entities;
using PostoUNICEUB.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Role mock (via argumento)
string roleMock = "admin";
if (args.Length > 0)
{
    var roleArg = args[0].ToLower();
    var rolesValidos = new[] { "medico", "enfermeiro", "admin" };
    if (rolesValidos.Contains(roleArg))
    {
        roleMock = roleArg;
    }
}
builder.Services.AddDistributedMemoryCache(); // <- ESSENCIAL!
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRoleService, SessionUserRoleService>();


// 🔹 Serviços
builder.Services.AddDbContext<PostoCeubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSqlServer")));

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Sessão configurada
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🔹 Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();       // ✅ Sessão precisa vir ANTES da autorização
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapRazorPages();

app.Run();
