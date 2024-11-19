using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using MiniCourseSalesProject.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Key"))).SetDefaultKeyLifetime(TimeSpan.FromDays(30));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "Mini_Course_Cookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.LoginPath = "/Auth/SignIn";
        //options.AccessDeniedPath = "/Home/AccessDenied";
    });
builder.Services.AddHttpClient<AuthService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
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
