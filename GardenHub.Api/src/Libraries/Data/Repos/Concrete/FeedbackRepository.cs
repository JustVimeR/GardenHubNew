using Data.Contexts;
using Data.Repos.Interfaces;
using Models.DbEntities;

namespace Data.Repos.Concrete
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext dataContext) : base(dataContext)
        {

        }
    }
}
