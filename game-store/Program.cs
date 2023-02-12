using game_store.Infrastructure;
using game_store_domain;
using game_store_domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GameStoreDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:GAMESTORE_DB_CONN_STR"]);
});

builder.Services.AddScoped<IGameServices, GameServiceProvider>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Services
   .CreateScope()
   .ServiceProvider
   .GetRequiredService<GameStoreDbContext>()
   .EnsurePopulatedWithDemoData();

app.Run();
