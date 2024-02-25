using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;

namespace Services.GardenhubServices;

public class WorkTypeService : Service<WorkType>, IWorkTypeService
{
    public WorkTypeService(IWorkTypeRepository repository) : base(repository)
    {
    }
}