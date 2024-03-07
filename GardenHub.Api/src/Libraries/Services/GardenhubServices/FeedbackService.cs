using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class FeedbackService : Service<Feedback>, IFeedbackService
{
    private readonly IProjectService _projectService;
    private readonly IUserAccessor _userAccessor;

    public FeedbackService(IFeedbackRepository repository, IUserAccessor userAccessor,
        IProjectService projectService) : base(repository)
    {
        _projectService = projectService;
        _userAccessor = userAccessor;
    }

    public override async Task<Feedback> PostAsync(Feedback addFeedback)
    {
        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        Project project = await _projectService.GetFirstAsync(x => x.Id == addFeedback.ProjectId);

        if (project.CustomerId != userProfile.CustomerProfileId)
        {
            throw new ApiException(
               (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotReferenceNotOwnedEntity,
                                                                       nameof(Project), project.Id);
        }

        GardenerProfile? gardener = project.Gardeners!.FirstOrDefault(x => x.Id == addFeedback.GardenerId);

        if (gardener == null)
        {
            throw new ApiException(
               (int)HttpStatusCode.BadRequest, ErrorMessages.GardenerNotRelatedToProject,
                                                                        addFeedback.GardenerId, project.Id);
        }

        addFeedback.CustomerId = userProfile.CustomerProfileId;

        return await base.PostAsync(addFeedback);
    }

    public override async Task DeleteAsync(Feedback feedback, bool softDelete = false)
    {
        UserProfile userProfile = await _userAccessor.GetUserProfileAsync();

        if (feedback.CustomerId!=userProfile.CustomerProfileId)
        {
            throw new ApiException(
                (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotDeleteNotOwnedEntity,
                                                                            nameof(Feedback), feedback.Id);
        }

        await base.DeleteAsync(feedback, softDelete);
    }
}