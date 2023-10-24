using ElementFitness.App;
using ElementFitness.BL.Interfaces;
using ElementFitness.BL.Services;
using ElementFitness.DAL.Data;
using ElementFitness.DAL.Interfaces;
using ElementFitness.DAL.Repositories;
using ElementFitness.Utils.Configurations;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Injecting Services & Repos
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IOfferService, OfferService>();

builder.Services.AddScoped<IProgramRepo, ProgramRepo>();
builder.Services.AddScoped<IOfferRepo, OfferRepo>();


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

TypeAdapterConfig<ElementFitness.Models.Program, ElementFitness.Models.Program>
    .NewConfig()
    .IgnoreIf((src, dest) => src.ProgramID == 0, dest => dest.ProgramID)
    .IgnoreNullValues(true);

TypeAdapterConfig<ElementFitness.Models.Offer, ElementFitness.Models.Offer>
    .NewConfig()
    .IgnoreIf((src, dest) => src.OfferID == 0, dest => dest.OfferID)
    .IgnoreNullValues(true);

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
