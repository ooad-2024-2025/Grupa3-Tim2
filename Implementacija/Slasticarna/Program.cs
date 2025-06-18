using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Slasticarna.Data;
using Slasticarna.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// konekcija na bazu
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity i podršku za role 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthorization();

var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bin/libwkhtmltox.dll"));

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
