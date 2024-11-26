using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using MiniCourseSalesProject.Web.Models.Handler;
using MiniCourseSalesProject.Web.Models.Services;
using MiniCourseSalesProject.Web.Models.Validations;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
//þifreleme anahtarýný buraya kaydet
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Key"))).SetDefaultKeyLifetime(TimeSpan.FromDays(30));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "Mini_Course_Cookie";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.LoginPath = "/Auth/SignIn";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

builder.Services.AddAuthorization();



builder.Services.AddHttpClient<AuthService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();
builder.Services.AddHttpClient<CourseService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();
builder.Services.AddHttpClient<CategoryService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();
builder.Services.AddHttpClient<UserService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();
builder.Services.AddHttpClient<OrderService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();
builder.Services.AddHttpClient<PaymentService>(x =>
{
    x.BaseAddress = new Uri(builder.Configuration.GetSection("ApiOption")["BaseAdress"]!);
}).AddHttpMessageHandler<ClientCredentialHandler>();


builder.Services.AddMemoryCache();
builder.Services.AddScoped<ClientCredentialHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<SignInValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SignUpValidator>();

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
