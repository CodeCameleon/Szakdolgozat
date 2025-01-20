using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.UnitofWork.Extensions;

﻿// Létrehoz egy webalkalmazás építőt.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Hozzáadja az adatbázis kontextust a konténerhez.
builder.Services.AddDbContext(GlobalConfiguration.DefaultConnection);

// Hozzáadja az adattárakat a konténerhez.
builder.Services.AddRepositories();

// Hozzáadja az egységmunkát a konténerhez.
builder.Services.AddUnitofWork();

// Hozzáadja a szolgáltatásokat a konténerhez.
builder.Services.AddServices();

// Hozzáadja a vezérlőket és a nézeteket a konténerhez.
builder.Services.AddControllersWithViews();

// Befejezi az alkalmazás építését.
WebApplication app = builder.Build();

// Létrehozza az adatbázist és alkalmazza a migrációkat.
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    TestResultsDbContext context = services.GetRequiredService<TestResultsDbContext>();
    context.Database.Migrate();
}

// Beállítja a hibakezelést a HTTP kérés csővezetékben.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Hozzáadja a HTTP átírányító köztes szoftvert.
app.UseHttpsRedirection();

// Engedélyezi a statikus fájlok kiszolgálását.
app.UseStaticFiles();

// Hozzáadja a navigációs köztes szoftvert.
app.UseRouting();

// Hozzáadja az engedélyező köztes szoftvert.
app.UseAuthorization();

// Beállítja az alapértelmezet útvonalat.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Elindítja az alkalmazást.
app.Run();
