using Matricula.Models;
using Microsoft.EntityFrameworkCore;

using Matricula.Servicios.Contrato;
using Matricula.Servicios.Implementacion;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MatriculaAdexContext>(options =>
{
    //Cadena de conexión
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdexContext"));
});

//Usar Interfaz y Clase que la implementa en cualquier Controlador.
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

//Usar cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Alumnos/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

//Borrar cookie
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(
            new ResponseCacheAttribute
            {
                NoStore = true,
                Location = ResponseCacheLocation.None,
            }
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Usar autenticación
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Alumnos}/{action=IniciarSesion}/{id?}");

app.Run();
