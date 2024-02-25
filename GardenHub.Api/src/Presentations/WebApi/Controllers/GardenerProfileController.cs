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
using Services.GardenhubServices.Interfaces;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GardenerProfileController : GenericCRUDDController<GardenerProfile, GetGardenerProfileDTO, PostGardenerProfileDTO, GardenerProfile>
{
    ILogger<UserProfileController> _logger;

    public GardenerProfileController(
    IGardenerProfileService service,
    IMapper mapper, FilterService filterService,
    ILogger<UserProfileController> logger)
    : base(service, mapper, filterService)
    {
        _logger = logger;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task<ActionResult<ServiceResult<GetGardenerProfileDTO>>> PostAsync(PostGardenerProfileDTO addDto)
    {
        return await base.PostAsync(addDto);
    }
}
