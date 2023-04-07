using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using game_store_domain.Entities;
using game_store_domain.Data;
using game_store.Infrastructure;
using game_store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<GameStoreUser, IdentityRole<int>>(opt =>
{
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
builder.Services.AddMvc();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<GameStoreDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:AZURE_SQL_CONNECTIONSTRING"]);
});

var app = builder.Build();

// Create some test data if database is empty
// TODO: Remove on production
DataSeeder.Init(app);

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

app.MapRazorPages();

app.Run();