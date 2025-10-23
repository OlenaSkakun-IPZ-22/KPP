using Microsoft.AspNetCore.Authentication.Cookies;
using Okta.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProcureRiskAnalyzer.Web.Data;


var builder = WebApplication.CreateBuilder(args);

// Додаємо MVC
builder.Services.AddControllersWithViews();

// Налаштовуємо авторизацію через Okta OAuth2
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
})
.AddCookie()
.AddOktaMvc(new OktaMvcOptions
{
    OktaDomain = builder.Configuration["Okta:Domain"],
    ClientId = builder.Configuration["Okta:ClientId"],
    ClientSecret = builder.Configuration["Okta:ClientSecret"]
});
var dbProvider = builder.Configuration["DatabaseProvider"];

switch (dbProvider)
{
    case "SqlServer":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        break;
    case "Postgres":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
        break;
    case "Sqlite":
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
        break;
    default:
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("ProcureDB"));
        break;
}

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Маршрутизація
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
