using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos;

public class GeneralWorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
{
    public GeneralWorkTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }
}

