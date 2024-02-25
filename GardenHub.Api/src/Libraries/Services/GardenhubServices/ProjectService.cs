using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;

namespace Services.GardenhubServices;

public class ProjectService : Service<Project>, IProjectService
{
    public ProjectService(IProjectRepository repository) : base(repository)
    {
    }
}
