using AutoMapper;
using Core.Constants;
using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController
    : BaseCRUDController<UserProfile, GetUserProfileDTO, PostUserProfileDTO, PostUserProfileDTO>
{
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public UserProfileController(
    IUserProfileService userProfileService,
    IMapper mapper, FilterService filterService,
    IUserAccessor userAccessor)
    : base(userProfileService, mapper, filterService)
    {
        _userProfileService = userProfileService;
        _mapper = mapper;
         _userProfileService = userProfileService;
        _userAccessor = userAccessor;
    }

    [NonAction]
    public override Task<ActionResult<ServiceResult<GetUserProfileDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }

    [NonAction]
    public override Task<ActionResult<ServiceResult<GetUserProfileDTO>>> PostAsync(PostUserProfileDTO addDto)
    {
        return base.PostAsync(addDto);
    }

    [Authorize]
    public override async Task<ActionResult<ServiceResult<GetUserProfileDTO>>> PutAsync(
        [FromRoute, Required] long id, PostUserProfileDTO updateUserProfile)
    {
        if (_userAccessor.UserProfileId != id)
        {
            throw new ApiException(
               (int)HttpStatusCode.BadRequest, ErrorMessages.CouldNotUpdateNotOwnedEntity,
                                                                        nameof(UserProfile), id);
        }

        return await base.PutAsync(id, updateUserProfile);
    }

    [HttpGet("gardeners")]
    public async Task<ActionResult<ServiceResult<List<GetUserProfileDTO>>>> GetGardeners()
    {
        ServiceResult<List<GetUserProfileDTO>> serviceResult = new ServiceResult<List<GetUserProfileDTO>>();

        List<UserProfile> userProfiles = await _userProfileService.GetGardenerProfiles();

        serviceResult.Data = _mapper.Map<List<GetUserProfileDTO>>(userProfiles);

        return serviceResult;
    }

    [Authorize]
    [HttpGet("getself")]
    public async Task<ActionResult<ServiceResult<GetUserProfileDTO>>> GetSelfAsync()
    {
        ServiceResult<GetUserProfileDTO> serviceResult = new ServiceResult<GetUserProfileDTO>();

        UserProfile userProfile = await _userProfileService.GetUserProfileFromToken();

        serviceResult.Data = _mapper.Map<GetUserProfileDTO>(userProfile);

        return Ok(serviceResult);
    }
}