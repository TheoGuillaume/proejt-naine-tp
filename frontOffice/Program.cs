using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true; // Assurez-vous que le cookie est essentiel
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Assurez-vous que le cookie est envoyé uniquement via HTTPS
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Ajustez la durée d'expiration selon vos besoins
        options.LoginPath = "/Home/Index"; // Redirection vers la page de connexion si l'authentification est nécessaire
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
