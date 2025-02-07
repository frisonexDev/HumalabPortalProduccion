using GDifare.Portales.HumaLab.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using GDifare.Portal.Humalab.Servicio.Modelos;
using NuGet.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

AppSettings? set = config.GetRequiredSection("SRVMICROSERVICIOS").Get<AppSettings>();
AppConfigurations? opts = config.GetRequiredSection("APPCONFIGURATION").Get<AppConfigurations>();
AppServicioMicros? setMicroservicios = config.GetRequiredSection("SRVMICROGESTIONCLIENTE").Get<AppServicioMicros>();
AppServicioMicrosExternos? setMicrosExternos = config.GetRequiredSection("SRVMICROEXTERNOS").Get<AppServicioMicrosExternos>();
AppServicioClienteApi? setMicroCliente = config.GetRequiredSection("SRVMICROCLIENTEAPI").Get<AppServicioClienteApi>();
Variables? variables = config.GetRequiredSection("VARCONFIGURATION").Get<Variables>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<AppSettings>(set); //variables globales
builder.Services.AddSingleton<AppServicioMicros>(setMicroservicios); //micros internos humalab
builder.Services.AddSingleton<AppServicioMicrosExternos>(setMicrosExternos); //micros externos
builder.Services.AddSingleton<AppServicioClienteApi>(setMicroCliente); //micro Cliente
builder.Services.AddSingleton<Variables>(variables); //variables de configuracion

builder.Services.AddRazorPages();

// Servicios MVC
builder.Services.AddControllers();

// HealthChecks
builder.Services.AddHealthChecks();

builder.Services.AddMemoryCache();


builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromMinutes(opts.TimeSession);
});


builder.Services.AddAuthentication("Identity.Application")
               .AddCookie("Identity.Application", options =>
               {
                   options.LoginPath = new PathString("/");
                   options.LogoutPath = new PathString("/Logout");
                   options.ExpireTimeSpan = TimeSpan.FromMinutes(opts.TimeCookie);
               });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    string EnteredPath = context.Request.HttpContext.Request.Path.ToString().Remove(0, 1);
    await next();

    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Login/Error";
        await next();
    }
});
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}");

app.Run();

