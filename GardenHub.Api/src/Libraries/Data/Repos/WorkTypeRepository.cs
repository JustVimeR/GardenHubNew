using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
{
    public WorkTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<WorkType> PrepareDbSet()
    {
        return base.PrepareDbSet()
            .Where(x => x.ParentWorkTypeId == null)
            .Include(x => x.DerivedWorkTypes);
    }
}

