using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using game_store_domain.Entities;
using game_store_domain.Data;
using Data.Interfaces;
using AutoMapper;
using Business;
using System.Reflection;
using game_store_business.ServiceInterfaces;
using game_store_business.Services;
using game_store.Infrastructure;
using game_store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<GameStoreUser, IdentityRole<int>>(opt => {
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 4;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<GameStoreDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddGameStoreServices();

// Create some test data if database is empty
// TODO: Remove on production
builder.Services.AddTransient<SeedData>();

builder.Services.AddMvc();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<GameStoreDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:GAMESTORE_DB_CONN_STR"]);
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

//app.MapControllerRoute(
//    name: "games",
//    pattern: "{controller=Game}/{action=Index}/{FilterOptions?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

// TODO: Remove on production
app.Services.GetService<SeedData>().Init(app.Services.GetService<GSUnitOfWork>());
