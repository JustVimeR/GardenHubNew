using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }
    protected override IQueryable<City> PrepareDbSet()
    {
        return base.PrepareDbSet().Include(x => x.Gardeners);
    }
}
