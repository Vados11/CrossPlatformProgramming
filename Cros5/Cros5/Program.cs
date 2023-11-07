using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Okta.AspNetCore;
using Cros5.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.UseUrls("https://*:7047");
//builder.Services.Configure<OktaTokenSettings>(builder.Configuration.GetSection("Okta"));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
})
.AddAuthentication(authOptions =>
{
    authOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOktaMvc(new OktaMvcOptions
{
    // add Okta information
    AuthorizationServerId = "default",
    ClientId = "0oad4qku0iFm3TMoK5d7",
    OktaDomain = "https://dev-90762946.okta.com",
    ClientSecret = "XNG6IdqDsQCn6c0ybq6oYdoEwQaRdYYj_P1-lzxcoVDiKqz_sqgSEnkZUJSx4kCV",
    Scope = new List<string> { "openid", "profile", "email", "phone" },
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

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