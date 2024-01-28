using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}
