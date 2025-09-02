using CIDM_3312_FinalProject.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRazorPages();
// Bring in database context with dependency injection.
builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TicketConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
        SeedData.Initialize(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
