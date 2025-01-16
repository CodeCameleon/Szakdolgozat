// Létrehoz egy webalkalmazás építőt.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Hozzáadja a vezérlőket és a nézeteket a konténerhez.
builder.Services.AddControllersWithViews();

// Befejezi az alkalmazás építését.
WebApplication app = builder.Build();

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
