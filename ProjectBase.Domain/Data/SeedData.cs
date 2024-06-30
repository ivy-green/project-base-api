using ProjectBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProjectBase.Domain.Data
{
    public static class SeedData
    {
        public static ModelBuilder SeedingData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData([
                    new Role {
                        Id = 1,
                        RoleName = "Admin",
                    },
                    new Role {
                        Id = 2,
                        RoleName = "Manager",
                    }]);

            modelBuilder.Entity<User>()
                .HasData([
                    new User {
                        Username = "admin",
                        Email = "admin@gmail.com",
                        Fullname = "admin",
                        PasswordHash = "123456",
                        PasswordSalt = "1234567890",
                        VerifyToken = "123131123123123123",
                        ResetPasswordToken = "123123123124243434"
                    }]);

            modelBuilder.Entity<UserRole>()
                .HasData([
                    new UserRole {
                        Username = "admin",
                        RoleId = 1
                    }]);

            return modelBuilder;
        }
    }
}
