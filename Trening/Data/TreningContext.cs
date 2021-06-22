using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trening.Areas.Identity.Data;
using Trening.Models;

namespace Trening.Data
{
    public class TreningContext : IdentityDbContext<TreningKorisnik>
    {
        public TreningContext (DbContextOptions<TreningContext> options)
            : base(options)
        {
        }

        public DbSet<Trening.Models.Coach> Coach { get; set; }

        public DbSet<Trening.Models.Discipline> Discipline { get; set; }

        public DbSet<Trening.Models.User> User { get; set; }

        public DbSet<Trening.Models.Training> Training { get; set; }
        public DbSet<Trening.Models.Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Coach>().ToTable("Coach");
            modelBuilder.Entity<Discipline>().ToTable("Discipline");
            modelBuilder.Entity<Training>().ToTable("Training");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");


            modelBuilder.Entity<Training>()
                    .HasOne(m => m.Coach)
                    .WithMany(t => t.Training)
                    .HasForeignKey(m => m.CoachID).OnDelete(DeleteBehavior.NoAction);
            // .WillCascadeOnDelete(false);

            modelBuilder.Entity<Enrollment>()
            .HasOne(m => m.Training)
                    .WithMany(t => t.Enrollments)
                    .HasForeignKey(m => m.TrainingID).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
          .HasOne(m => m.User)
                  .WithMany(t => t.Enrollments)
                  .HasForeignKey(m => m.UserID).OnDelete(DeleteBehavior.NoAction);
        }

    }
}
