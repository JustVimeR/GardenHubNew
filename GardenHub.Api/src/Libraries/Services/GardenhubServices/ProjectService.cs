using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Data.Repos.Interfaces;
using Models.DbEntities;
using Services.GardenhubServices.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class ProjectService : Service<Project>, IProjectService
{
    private readonly IMapper _mapper;
    private readonly IWorkTypeService _workTypeService;
    private readonly IUserProfileService _userProfileService;

    public ProjectService(IProjectRepository repository, IUserProfileService userProfileService,
        IWorkTypeService workTypeService, IMapper mapper)
        : base(repository)
    {
        _mapper = mapper;
        _workTypeService = workTypeService;
        _userProfileService = userProfileService;
    }

    public async override Task<Project> PostAsync(Project addProject)
    {
        UserProfile userProfile = await _userProfileService.GetUserProfileFromToken();

        addProject.Customer = userProfile;

        if (addProject.WorkTypes != null)
        {
            addProject.WorkTypes = await _workTypeService.GetDerivedWorkTypesById(
                addProject.WorkTypes
                .Select(x => x.Id)
                .ToList());
        }

        addProject.PublicationDate = DateTime.UtcNow;
        addProject.IsVerified = true;

        return await base.PostAsync(addProject);
    }

    public override async Task<Project> PutAsync(Project updateProject)
    {
        if (updateProject.Id == default)
        {
            throw new ApiException(
                (int)HttpStatusCode.BadRequest, ErrorMessages.UpdateEntityWithNoId, nameof(Project));
        }

        Project project = await base.GetFirstAsync(x => x.Id == updateProject.Id);
        
        UserProfile userProfile = await _userProfileService.GetUserProfileFromToken();

        if (project.CustomerId != userProfile.Id)
        {
            throw new ApiException(
                (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotUpdateNotOwnedEntity,
                                                                        nameof(Project), updateProject.Id);
        }

        _mapper.Map(updateProject, project);

        if (project.WorkTypes != null)
        {
            project.WorkTypes = await _workTypeService.GetDerivedWorkTypesById(
                project.WorkTypes
                .Select(x => x.Id)
                .ToList());
        }

        return await base.PutAsync(project);
    }

    public override async Task DeleteAsync(Project project, bool softDelete = false)
    {
        UserProfile userProfile = await _userProfileService.GetUserProfileFromToken();

        if (project.CustomerId != userProfile.Id)
        {
            throw new ApiException(
                (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotDeleteNotOwnedEntity,
                                                                        nameof(Project), project.Id);
        }

        await base.DeleteAsync(project, softDelete);
    }
}
