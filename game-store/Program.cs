using game_store_domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using game_store;
using game_store_domain.Entities;
using Microsoft.Extensions.Options;
using game_store_domain.Data;

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

builder.Services.AddMvc();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<GameStoreDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:GAMESTORE_DB_CONN_STR"]);
});

builder.Services.AddScoped<IGameStoreServices, GameStoreServiceProvider>();


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
