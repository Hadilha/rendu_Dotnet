using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1) Ajouter le DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Enregistrer l’interface utilisée dans les services
builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

// 3) Enregistrer les services métiers
builder.Services.AddScoped<IBilanService, BilanService>();
builder.Services.AddScoped<IInfirmierService, InfirmierService>();
builder.Services.AddScoped<IPatientService, PatientService>();

// 4) Ajouter MVC
builder.Services.AddControllersWithViews();

// 5) Configurer les routes
var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Infirmier}/{action=Index}/{id?}");

app.Run();
