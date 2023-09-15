using ElementFitness.App;
using ElementFitness.DAL.Data;
using ElementFitness.Utils.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(AppSettings.PostGresConnectionString, b => b.MigrationsAssembly("ElementFitness.App"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Host.UseSerilog();
IConfiguration configuration = builder.Configuration;
AppSettings.IntializeConfiguration(configuration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(AppSettings.SerilogOutputFilePath, rollingInterval: RollingInterval.Infinite)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();
await InitialSetup.BuildDefaultIdentityAsync(app.Services.CreateScope().ServiceProvider);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
