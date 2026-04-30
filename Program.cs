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

// Configuración de BD Real usando SQL Server
builder.Services.AddDbContext<Conexion>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexionSQL")));

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
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICanchaService, CanchaService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"); // Página principal predeterminada
});

// ==========================================
// SECCIÓN DE SEED DATA (DATOS INICIALES)
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Conexion>();

    // Aplica las migraciones pendientes automáticamente
    context.Database.Migrate();

    // 1. Verificamos y creamos el admin
    if (!context.Usuarios.Any(u => u.NombreUsuario == "admin"))
    {
        context.Usuarios.Add(new Usuario { NombreUsuario = "admin", Clave = "123", Rol = "Admin" });
    }
    
    // 2. Verificamos y creamos el cliente
    if (!context.Usuarios.Any(u => u.NombreUsuario == "cliente"))
    {
        context.Usuarios.Add(new Usuario { NombreUsuario = "cliente", Clave = "123", Rol = "Cliente" });
    }
    context.SaveChanges(); // Guardamos para que se generen los IDs

    // 3. Creamos el perfil de Cliente asociado al usuario "cliente"
    var usuarioCliente = context.Usuarios.FirstOrDefault(u => u.NombreUsuario == "cliente");
    if (usuarioCliente != null && !context.Clientes.Any(c => c.IdUsuario == usuarioCliente.IdUsuario))
    {
        context.Clientes.Add(new Cliente { Nombres = "Juan", Apellidos = "Pérez", DNI = "87654321", Telefono = "999888777", Correo = "juan@test.com", IdUsuario = usuarioCliente.IdUsuario });
        context.SaveChanges();
    }

    // 4. Verificamos y creamos una empresa de prueba
    if (!context.Usuarios.Any(u => u.NombreUsuario == "empresa"))
    {
        context.Usuarios.Add(new Usuario { NombreUsuario = "empresa", Clave = "123", Rol = "Empresa" });
        context.SaveChanges();
    }
    
    var usuarioEmpresa = context.Usuarios.FirstOrDefault(u => u.NombreUsuario == "empresa");
    var empresaTest = context.Empresas.FirstOrDefault(e => e.Nombre == "Sport Center");
    if (usuarioEmpresa != null && empresaTest == null)
    {
        empresaTest = new Empresa { Nombre = "Sport Center", RUC = "12345678901", Direccion = "Av. Ficticia 123", Telefono = "987654321", Email = "info@sportcenter.com", IdUsuario = usuarioEmpresa.IdUsuario, FechaRegistro = DateTime.Now };
        context.Empresas.Add(empresaTest);
        context.SaveChanges();
    }

    // 5. Creamos unas canchas para que no se vea vacío
    if (!context.Canchas.Any() && empresaTest != null)
    {
        context.Canchas.AddRange(
            new Cancha { Nombre = "Estadio Monumental", TipoDeporte = "Fútbol 11", PrecioHora = 120, Estado = "Disponible", Descripcion = "Cancha de césped natural profesional.", EmpresaId = empresaTest.EmpresaId },
            new Cancha { Nombre = "La Bombonera", TipoDeporte = "Fútbol 7", PrecioHora = 80, Estado = "Disponible", Descripcion = "Sintético ideal para pichangas nocturnas.", EmpresaId = empresaTest.EmpresaId }
        );
    }

    // 6. Creamos algunos Horarios base (ej. formato 24h para turnos de noche)
    if (!context.Horarios.Any())
    {
        context.Horarios.AddRange(
            new Horario { HoraInicio = new TimeSpan(18, 0, 0), HoraFin = new TimeSpan(19, 0, 0), Estado = "Disponible" },
            new Horario { HoraInicio = new TimeSpan(19, 0, 0), HoraFin = new TimeSpan(20, 0, 0), Estado = "Disponible" },
            new Horario { HoraInicio = new TimeSpan(20, 0, 0), HoraFin = new TimeSpan(21, 0, 0), Estado = "Disponible" },
            new Horario { HoraInicio = new TimeSpan(21, 0, 0), HoraFin = new TimeSpan(22, 0, 0), Estado = "Disponible" }
        );
    }

    context.SaveChanges();
}
// ==========================================

app.Run();