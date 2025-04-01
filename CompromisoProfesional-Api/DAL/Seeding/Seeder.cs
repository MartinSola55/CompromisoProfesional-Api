using Microsoft.EntityFrameworkCore;
using CompromisoProfesional_Api.DAL.DB;
using CompromisoProfesional_Api.Models;
using CompromisoProfesional_Api.Models.Constants;

namespace CompromisoProfesional_Api.DAL.Seeding
{
    public class Seeder(APIContext db, IConfiguration config) : ISeeder
    {
        private readonly APIContext _db = db;
        private readonly IConfiguration _config = config;

        public async Task Seed()
        {
            try
            {
                var tx = await _db.Database.BeginTransactionAsync();

                var migrations = await _db.Database.GetPendingMigrationsAsync();

                if (migrations.Any())
                    _db.Database.Migrate();

                // Executed only once when the database is created
                if (await _db.Role.AnyAsync(x => x.Name == Roles.ADMIN))
                    return;

                // Create roles
                _db.Role.AddRange(Roles.GetRoles().Select(x => new Role { Name = x }));

                await _db.SaveChangesAsync();

                var roleId = _db
                    .Role
                    .Where(x => x.Name == Roles.ADMIN)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                var email = _config["AdminEmail"] ?? "admin@admin";
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(_config["AdminPassword"] ?? "Password1!");
;
                // Create admin user
                User admin = new()
                {
                    Email = email,
                    Name = "Admin",
                    LastName = "Admin",
                    RoleId = roleId,
                    PasswordHash = passwordHash,
                };

                _db.User.Add(admin);

                SeedProfessions();

                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _db.Database.RollbackTransactionAsync();
                throw;
            }
        }

        #region DB Seed
        private void SeedProfessions()
        {
            _db.Profession.AddRange([
                new Profession { Name = "Médico", Type = ProfessionType.VISIT },
                new Profession { Name = "Cuidador/a", Type = ProfessionType.HOUR },
                new Profession { Name = "Kinesiólogo", Type = ProfessionType.VISIT }
            ]);
        }
        #endregion
    }
}