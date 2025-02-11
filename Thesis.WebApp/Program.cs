using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.UnitOfWork.Extensions;
using Thesis.WebApp.Services.Implementations;
using Thesis.WebApp.Services.Interfaces;

// Létrehoz egy webalkalmazás építőt.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Hozzáadja a globális konfigurációt az építőhöz.
builder.Configuration.AddConfiguration(GlobalConfiguration.Configuration);

// Hozzáadja az adatbázis kontextust a konténerhez.
builder.Services.AddDbContext(GlobalConfiguration.DefaultConnection);

// Hozzáadja az adattárakat a konténerhez.
builder.Services.AddRepositories();

// Hozzáadja az egységmunkát a konténerhez.
builder.Services.AddUnitOfWork();

// Hozzáadja a szolgáltatásokat a konténerhez.
builder.Services.AddServices();

// Hozzáadja a teszteseteket lértehozó eszközt a konténerhez.
builder.Services.AddTransient<ITestInputGenerator, TestInputGenerator>();

// Hozzáadja az NUnit teszteket futtató szolgáltatást a konténerhez.
builder.Services.AddScoped<ITestRunnerService, TestRunnerService>();

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
