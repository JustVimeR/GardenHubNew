using Models.DbEntities;
using System.Threading.Tasks;

namespace Services.GardenhubServices.Interfaces;

public interface IProjectService : IService<Project>
{
    Task AcceptProjectApply(long customerId, long projectId, long gardenerId);
}
