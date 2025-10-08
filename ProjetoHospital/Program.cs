using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoHospital;
using ProjetoHospital.Entities;
using ProjetoHospital.Seeds;
using ProjetoHospital.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters();
    });

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var serverVersion = new MySqlServerVersion(new Version(12, 0, 2));

builder.Services.AddDbContext<ProjetoHospitalContext>(opt =>
    opt.UseMySql(builder.Configuration.GetConnectionString("SqlServerConnection"), serverVersion));

builder.Services.AddIdentity<Usuario, Role>()
    .AddEntityFrameworkStores<ProjetoHospitalContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IGenericRepository<Setor>, GenericRepository<Setor>>();
builder.Services.AddScoped<IGenericRepository<Quarto>, GenericRepository<Quarto>>();
builder.Services.AddScoped<IGenericRepository<Leito>, GenericRepository<Leito>>();
builder.Services.AddScoped<IGenericRepository<Limpeza>, GenericRepository<Limpeza>>();
builder.Services.AddScoped<IGenericRepository<Usuario>, GenericRepository<Usuario>>();
builder.Services.AddScoped<IGenericRepository<Manutencao>, GenericRepository<Manutencao>>();

builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IQuartoService, QuartoService>();
builder.Services.AddScoped<ILeitoService, LeitoService>();
builder.Services.AddScoped<ILimpezaService, LimpezaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IManutencaoService, ManutencaoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjetoHospitalContext>();
    db.Database.Migrate();
    await SeedRoles.InitializeAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
