using Microsoft.EntityFrameworkCore;
using ThePlayfulPawn.Data;
using ThePlayfulPawn.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PawnDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PawnRepo<Game>>();
builder.Services.AddScoped<PawnRepo<Address>>();
builder.Services.AddScoped<PawnRepo<Customer>>();
builder.Services.AddScoped<PawnRepo<Food>>();
builder.Services.AddScoped<PawnRepo<Vendor>>();
builder.Services.AddScoped<PawnRepo<Reservation>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
