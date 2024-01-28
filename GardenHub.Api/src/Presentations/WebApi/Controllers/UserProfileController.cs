using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : GenericCRUDDController<UserProfile, GetUserProfileDTO, GetUserProfileDTO, PostUserProfileDTO,UserProfile>
    {
        ILogger<UserProfileController> _logger;
        IUserAccessor _userAccessor;
        IMapper _mapper;

        public UserProfileController(
        IUserProfileService service,
        IMapper mapper,
        ILogger<UserProfileController> logger, IUserAccessor userAccessor)
        : base(service, mapper)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
            _logger = logger;
        }
        
        [NonAction]
        [Authorize]
        public override async Task<ActionResult<ServiceResult<GetUserProfileDTO[]>>> PostAsync(PostUserProfileDTO[] addDtos)
        {
            return await base.PostAsync(addDtos);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getself")]
        public async Task<ActionResult<ServiceResult<GetUserProfileDTO>>> GetSelfAsync()
        {
            var serviceResult = new ServiceResult<GetUserProfileDTO>();
            int id = _userAccessor.IdentityUserId;
            var user = await service.GetFirstOrDefaultAsync(x=>x.IdentityId==id);
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
}

