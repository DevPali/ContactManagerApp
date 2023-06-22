using Microsoft.EntityFrameworkCore;

namespace ContactManagerApp.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Email> Emails { get; set; }
        //public DbSet<UserEmail> UserEmails { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleID);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ContactID);
            });

            //modelBuilder.Entity<UserEmail>(entity =>
            //{
            //    entity.HasKey(e => new { e.EmailId, e.UserId });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.UserEmails)
            //        .HasForeignKey(d => d.UserId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_UserEmails_Users");

            //    entity.HasOne(d => d.Email)
            //        .WithMany(p => p.UserEmails)
            //        .HasForeignKey(d => d.EmailId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_UserEmails_Emails");                
            //});

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ID);
            });
        }
    }
}
