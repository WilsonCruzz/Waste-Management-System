using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using waste_management_system.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
var services = builder.Services;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/UserProfiles/Login";
        options.LogoutPath = "/UserProfiles/Logout";
    })
    // Enable Google Auth
    .AddGoogle(options =>
    {
        // Access Google Auth section of appsettings.Development.json, therefore, 'googleAuth' will store the ClientID & Client Secret
        IConfigurationSection googleAuth = builder.Configuration.GetSection("Authentication:Google");

        // Read Google API Key values from config
        options.ClientId = configuration["GoogleOAuthClientId"];
        options.ClientSecret = configuration["GoogleOAuthClientKey"];
    })
    // Facebook Auth
    .AddFacebook(facebookOptions =>
     {
         facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
         facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
     });


//services.Configure<CookiePolicyOptions>(options =>
//{
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//    options.Secure = CookieSecurePolicy.Always;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();