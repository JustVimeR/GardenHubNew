using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.Interfaces;

namespace Services.Concrete
{
    public class FeedbackService : Service<Feedback>, IFeedbackService
    {
        public FeedbackService(IFeedbackRepository repository) : base(repository)
        {
        }
    }
}
