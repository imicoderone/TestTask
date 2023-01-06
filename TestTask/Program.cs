using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestTask.Application;
using TestTask.Application.Common;
using TestTask.Configurations;
using TestTask.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Access/Login";
        o.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("ProductChanges",
        policy => policy.RequireClaim(ClaimTypes.Role, RoleConstants.AdminRoleName)));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<ProductConfigurations>(builder.Configuration.GetSection(ProductConfigurations.ConfigurationName));

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
