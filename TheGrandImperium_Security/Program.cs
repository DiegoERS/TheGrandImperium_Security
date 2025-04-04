using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TheGrandImperium_Security.Core.Application;
using TheGrandImperium_Security.Core.Entities;
using TheGrandImperium_Security.Core.jwtLogic;
using TheGrandImperium_Security.Core.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// === Configuración de Base de Datos ===
builder.Services.AddDbContext<SecurityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSeguridad")));

// === Configuración de Identity ===
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<SecurityContext>()
    .AddDefaultTokenProviders();

// === Configuración de JWT ===
builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Interfaces y Servicios del Módulo de Seguridad
builder.Services.AddScoped<IJWTGenerator, JWTGenerator>(); // Generación de JWT
builder.Services.AddScoped<UsuarioActual>(); // Lógica de Usuarios
builder.Services.AddScoped<Login>(); // Autenticación
builder.Services.AddScoped<Register>(); // Registro de Usuarios
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


// === Migraciones Automáticas ===
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var contexto = services.GetRequiredService<SecurityContext>();
    contexto.Database.Migrate();
}

// === Configuración del Middleware ===
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
