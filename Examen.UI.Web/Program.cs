using Examen.Infrastructure;
using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Microsoft.EntityFrameworkCore;
using Examen.ApplicationCore.Interfaces;
using Examen.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext, EXContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInfirmierService, InfirmierService>();
builder.Services.AddScoped<IBilanService, BilanService>();
builder.Services.AddScoped<ILaboratoireService, LaboratoireService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddSingleton<Type>(t => typeof(GenericRepository<>));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
