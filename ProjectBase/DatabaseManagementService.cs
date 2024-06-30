using Microsoft.EntityFrameworkCore;
using ProjectBase.Domain.Data;

namespace ProjectBase
{
    public static class DatabaseManagementService
    {
        // Getting the scope of our database context
        public static void MigrationInitialisation(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var _Db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                if (_Db != null)
                {
                    if (_Db.Database.GetPendingMigrations().Any())
                    {
                        _Db.Database.Migrate();
                    }
                }
            }
        }
    }
}
