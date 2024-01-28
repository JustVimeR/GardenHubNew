using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}
