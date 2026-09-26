using Microsoft.EntityFrameworkCore;
using MVC_trabajo.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar el contexto de base de datos con la cadena de conexión
builder.Services.AddDbContext<AppDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBiblioteca")));

// Agregar soporte para sesiones (se usará en el login)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opciones =>
{
    opciones.IdleTimeout = TimeSpan.FromMinutes(30);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

// Registrar los servicios de MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de peticiones HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Activar el uso de sesiones antes de la autorización
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
