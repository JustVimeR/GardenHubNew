using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;

namespace Services.Concrete
{
    public class ProjectService : Service<Project>, IProjectService
    {
        public ProjectService(IProjectRepository repository) : base(repository)
        {
        }
    }
}
