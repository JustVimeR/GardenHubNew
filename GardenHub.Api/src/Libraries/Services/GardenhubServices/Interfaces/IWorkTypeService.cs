using Models.DbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IWorkTypeService : IService<WorkType>
{
    Task<List<WorkType>> GetDerivedWorkTypesById(List<long> workTypesIds);
}
