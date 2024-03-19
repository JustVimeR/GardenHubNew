using Data.Contexts;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using System.Linq;

namespace Data.Repos;

public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(ApplicationDbContext dataContext) : base(dataContext)
    {

    }

    protected override IQueryable<Feedback> PrepareDbSet()
    {
        return base.PrepareDbSet()
            .Include(x => x.Project)
            .Include(x => x.Gardener)
            .Include(x => x.Customer);
    }
}
