using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class DbMigrator
{
    private readonly ApplicationDbContext _dataContext;

    public DbMigrator(ApplicationDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void Migrate()
    {
        _dataContext.Database.Migrate();
    }
}
