using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;

namespace Services.Concrete
{
    public class WorkTypeService : Service<WorkType>, IWorkTypeService
    {
        public WorkTypeService(IWorkTypeRepository repository) : base(repository)
        {
        }
    }
}
