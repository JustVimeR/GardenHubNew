using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<Project> PrepareDbSet()
    {
        return base.PrepareDbSet()
            .Include(x => x.WorkTypes)
            .Include(x => x.Gardeners)
            .Include(x => x.Customer);
    }
}

