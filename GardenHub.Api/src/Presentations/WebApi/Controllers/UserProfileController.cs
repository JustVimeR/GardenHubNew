using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services;
using Services.GardenhubServices.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController
    : GenericCRUDDController<UserProfile, GetUserProfileDTO, PostUserProfileDTO, PostUserProfileDTO>
{
    IUserAccessor _userAccessor;
    IMapper _mapper;

    public UserProfileController(
    IUserProfileService userProfileService,
    IMapper mapper, FilterService filterService,
    IUserAccessor userAccessor)
    : base(userProfileService, mapper, filterService)
    {
        _userAccessor = userAccessor;
        _mapper = mapper;
    }


    [Authorize]
    public override async Task<ActionResult<ServiceResult<GetUserProfileDTO>>> PutAsync(
        [FromRoute, Required] long id, PostUserProfileDTO updateUserProfile)
    {
        return await base.PutAsync(id, updateUserProfile);
    }

    [Authorize]
    [HttpGet("getself")]
    public async Task<ActionResult<ServiceResult<GetUserProfileDTO>>> GetSelfAsync()
    {
        var serviceResult = new ServiceResult<GetUserProfileDTO>();
        int id = _userAccessor.IdentityUserId;
        var user = await service.GetFirstOrDefaultAsync(x => x.IdentityId == id);
        if (user == null)
        {
            serviceResult.Successful = false;
            serviceResult.Message = "User not found";
            return BadRequest(serviceResult);
        }

        var getUserDto = _mapper.Map<GetUserProfileDTO>(user);

        serviceResult.Data = getUserDto;

        return Ok(getUserDto);
    }
}