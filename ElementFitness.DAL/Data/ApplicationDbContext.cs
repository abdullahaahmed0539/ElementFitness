using ElementFitness.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ElementFitness.DAL.Data {
    public class ApplicationDbContext: IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){
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
            BuildIdentity(modelBuilder);
            BuildDatabase(modelBuilder);
        }
        private void BuildIdentity(ModelBuilder modelBuilder){

            string roleID = Guid.NewGuid().ToString(), userID = Guid.NewGuid().ToString();

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = roleID, Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = userID,
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "AdminPassword")
                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = roleID, 
                    UserId = userID
                }
            );
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