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
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Injecting Services & Repos
builder.Services.AddTransient<IProgramService, ProgramService>();
builder.Services.AddTransient<IOfferService, OfferService>();
builder.Services.AddTransient<ITrainerService, TrainerService>();
builder.Services.AddTransient<IPartnerService, PartnerService>();
builder.Services.AddTransient<IEnquiryService, EnquiryService>();
builder.Services.AddTransient<IJobListingService, JobListingService>();
builder.Services.AddTransient<IJobApplicantService, JobApplicantService>();
builder.Services.AddTransient<IMemberAndLeadService, MemberAndLeadService>();

builder.Services.AddTransient<IProgramRepo, ProgramRepo>();
builder.Services.AddTransient<IOfferRepo, OfferRepo>();
builder.Services.AddTransient<ITrainerRepo, TrainerRepo>();
builder.Services.AddTransient<IPartnerRepo, PartnerRepo>();
builder.Services.AddTransient<IEnquiryRepo, EnquiryRepo>();
builder.Services.AddTransient<IJobListingRepo, JobListingRepo>();
builder.Services.AddTransient<IJobApplicantRepo, JobApplicantRepo>();
builder.Services.AddTransient<IMemberAndLeadRepo, MemberAndLeadRepo>();



// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseNpgsql(AppSettings.PostGresConnectionString, b => b.MigrationsAssembly("ElementFitness.App"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Host.UseSerilog();
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


TypeAdapterConfig<ElementFitness.Models.Trainer, ElementFitness.Models.Trainer>
    .NewConfig()
    .IgnoreIf((src, dest) => src.TrainerID == 0, dest => dest.TrainerID)
    .IgnoreNullValues(true);

TypeAdapterConfig<ElementFitness.Models.Partner, ElementFitness.Models.Partner>
    .NewConfig()
    .IgnoreIf((src, dest) => src.PartnerID == 0, dest => dest.PartnerID)
    .IgnoreNullValues(true);

TypeAdapterConfig<ElementFitness.Models.Enquiry, ElementFitness.Models.Enquiry>
    .NewConfig()
    .IgnoreIf((src, dest) => src.EnquiryID == 0, dest => dest.EnquiryID)
    .IgnoreNullValues(true);

TypeAdapterConfig<ElementFitness.Models.Job, ElementFitness.Models.Job>
    .NewConfig()
    .IgnoreIf((src, dest) => src.JobID == 0, dest => dest.JobID)
    // .IgnoreIf((src, dest) => src.JobID == 0, dest => dest.JobID)
    .IgnoreNullValues(true);

TypeAdapterConfig<ElementFitness.Models.JobApplicant, ElementFitness.Models.JobApplicant>
    .NewConfig()
    .IgnoreIf((src, dest) => src.JobApplicantID == 0, dest => dest.JobApplicantID)
    // .IgnoreIf((src, dest) => src.JobApplicantID == 0, dest => dest.JobApplicantID)
    .IgnoreNullValues(true);


TypeAdapterConfig<ElementFitness.Models.Contact, ElementFitness.Models.Contact>
    .NewConfig()
    .IgnoreIf((src, dest) => src.ContactID == 0, dest => dest.ContactID)
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
