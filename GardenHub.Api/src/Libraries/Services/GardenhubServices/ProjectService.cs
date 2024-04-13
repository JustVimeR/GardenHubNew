using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Core.Extensions;
using Data.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services.GardenhubServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Services.GardenhubServices;

public class ProjectService : Service<Project>, IProjectService
{
    private readonly IMapper _mapper;
    private readonly IWorkTypeService _workTypeService;
    private readonly IUserProfileService _userProfileService;
    private readonly IChatService _chatService;

    public ProjectService(IProjectRepository repository, IUserProfileService userProfileService,
        IWorkTypeService workTypeService, IChatService chatService, IMapper mapper)
        : base(repository)
    {
        _chatService = chatService;
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

        _mapper.SelectiveMap<Project, PostProjectDTO>(updateProject, project);

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

    public async Task AcceptProjectApply(long customerId, long projectId, long gardenerId)
    {
        Project project = await base.GetFirstAsync(x => x.Id == projectId);

        if (project.CustomerId != customerId)
        {
            throw new ApiException(
                            (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotReferenceNotOwnedEntity,
                                                                                    nameof(Project), project.Id);
        }

        List<ChatMessage> customerNotifications = await _chatService.GetUserNotifications(customerId);

        ChatMessage? apply = customerNotifications.FirstOrDefault(x => x.SenderUserId == gardenerId &&
                                                         x.Message!.Contains(string.Format(Defaults.ApplyNotificationPrefix, projectId)));

        if (apply != null)
        {
            project.Gardeners!.Add(apply.SenderUser!);
            await base.PutAsync(project);
        }
        else
        {
            throw new ApiException(
                            (int)HttpStatusCode.BadRequest, ErrorMessages.GardenerNotAppliedToProject,
                                                                                    gardenerId, projectId);
        }
    }

}
