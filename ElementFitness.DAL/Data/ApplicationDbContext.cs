using ElementFitness.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ElementFitness.DAL.Data {
    public class ApplicationDbContext: IdentityDbContext {

        private readonly IServiceProvider _serviceProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IServiceProvider serviceProvider): base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<Contact>? Contacts  { get; set; }
        public DbSet<Trainer>? Trainers  { get; set; }
        public DbSet<Enquiry>? Enquiries  { get; set; }
        public DbSet<Job>? Jobs  { get; set; }
        public DbSet<JobApplicant>? JobApplicants  { get; set; }
        public DbSet<Offer>? Offers  { get; set; }
        public DbSet<Partner>? Partners  { get; set; }
        public DbSet<Program>? Programs  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildDatabase(modelBuilder);
        }

        private void BuildDatabase(ModelBuilder modelBuilder){
        
            modelBuilder.HasSequence<int>("auto_increment")
                    .StartsAt(1)
                    .IncrementsBy(1);

            modelBuilder.Entity<Contact>()
                .Property(contact => contact.ContactID)
                .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Trainer>()
                .Property(trainer => trainer.TrainerID)
                .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Enquiry>()
                .Property(enquiry => enquiry.EnquiryID)
                .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Job>()
            .Property(enquiry => enquiry.JobID)
            .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<JobApplicant>()
            .Property(enquiry => enquiry.JobApplicantID)
            .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Offer>()
            .Property(enquiry => enquiry.OfferID)
            .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Partner>()
            .Property(enquiry => enquiry.PartnerID)
            .HasDefaultValueSql("nextval('\"auto_increment\"')");

            modelBuilder.Entity<Program>()
            .Property(enquiry => enquiry.ProgramID)
            .HasDefaultValueSql("nextval('\"auto_increment\"')");

            base.OnModelCreating(modelBuilder);
        }
    }

}