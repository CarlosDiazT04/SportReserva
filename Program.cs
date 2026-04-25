using Microsoft.EntityFrameworkCore;
using SportReserva.Data;
using SportReserva.Repositories;
using SportReserva.Repositories.Interfaces;
using SportReserva.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using SportReserva.Models.Entities;
using SportReserva.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configuración de BD en Memoria para pruebas en Codespaces
builder.Services.AddDbContext<Conexion>(options =>
    options.UseInMemoryDatabase("BDSimulada"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";
        options.AccessDeniedPath = "/Login/Forbidden";
    });

// Inyección de Dependencias
builder.Services.AddScoped<ICanchaRepository, CanchaRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configuración de Cabeceras para Codespaces/Proxies
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | 
                       ForwardedHeaders.XForwardedProto | 
                       ForwardedHeaders.XForwardedHost
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// ==========================================
// SECCIÓN DE SEED DATA (DATOS INICIALES)
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Conexion>();

    // 1. Creamos un usuario para poder entrar
    if (!context.Usuarios.Any())
    {
        context.Usuarios.Add(new Usuario 
        { 
            NombreUsuario = "admin", 
            Clave = "123", 
            Rol = "Admin" 
        });
    }

    // 2. Creamos unas canchas para que no se vea vacío
    if (!context.Canchas.Any())
    {
        context.Canchas.AddRange(
            new Cancha { Nombre = "Estadio Monumental", TipoDeporte = "Fútbol 11", PrecioHora = 120, Estado = "Disponible", Descripcion = "Cancha de césped natural profesional." },
            new Cancha { Nombre = "La Bombonera", TipoDeporte = "Fútbol 7", PrecioHora = 80, Estado = "Disponible", Descripcion = "Sintético ideal para pichangas nocturnas." }
        );
    }

    context.SaveChanges();
}
// ==========================================

app.Run();