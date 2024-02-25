using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;

namespace Services.GardenhubServices;

public class FeedbackService : Service<Feedback>, IFeedbackService
{
    public FeedbackService(IFeedbackRepository repository) : base(repository)
    {
    }
}