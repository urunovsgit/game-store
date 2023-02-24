using game_store_domain;
using game_store_domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using game_store;
using game_store_domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<GameStoreDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:GAMESTORE_DB_CONN_STR"]);
});

builder.Services.AddScoped<IGameServices, GameServiceProvider>();
builder.Services.AddIdentity<GameStoreUser, IdentityRole>(opt => { opt.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<GameStoreDbContext>()
                .AddDefaultTokenProviders();

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

app.MapRazorPages();

app.Run();
