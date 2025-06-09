using DrugPreventionSystem.DataAccess.Utilities;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugPreventionSystem.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public object User { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consultant>()
                .HasOne(c => c.User)
                .WithOne(u => u.Consultant)
                .HasForeignKey<Consultant>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<Consultant>()
                .Property(c => c.ConsultationFee)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Consultant>()
                .Property(c => c.Rating)
                .HasColumnType("decimal(3,2)");

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin", Description = "System Administrator" },
                new Role { RoleId = 2, RoleName = "Manager", Description = "System Manager" },
                new Role { RoleId = 3, RoleName = "Staff", Description = "Staff Member" },
                new Role { RoleId = 4, RoleName = "Consultant", Description = "Professional Consultant" },
                new Role { RoleId = 5, RoleName = "Member", Description = "Registered Member" }
            );

            var hashedPassword = PasswordHasher.HashPassword("12345");
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "admin_user",
                    Email = "admin@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 1, // Admin role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "manager_user",
                    Email = "manager@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 2, // Manager role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "staff_user",
                    Email = "staff@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 3, // Staff role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "consultant_user",
                    Email = "consultant@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 4, // Consultant role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
