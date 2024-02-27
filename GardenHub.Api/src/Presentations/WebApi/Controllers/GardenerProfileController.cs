using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
public class GardenerProfileController : GenericCRUDDController<GardenerProfile, GetGardenerProfileDTO, PostGardenerProfileDTO, PostGardenerProfileDTO>
{
    private readonly IGardenerProfileService _gardenerProfileService;
    ILogger<UserProfileController> _logger;

    public GardenerProfileController(
    IGardenerProfileService gardenerProfileService,
    IMapper mapper, FilterService filterService,
    ILogger<UserProfileController> logger)
    : base(gardenerProfileService, mapper, filterService)
    {
        _gardenerProfileService = gardenerProfileService;
        _logger = logger;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task<ActionResult<ServiceResult<GetGardenerProfileDTO>>> PostAsync(PostGardenerProfileDTO addDto)
    {
        return await base.PostAsync(addDto);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task<ActionResult<ServiceResult<GetGardenerProfileDTO>>> PutAsync(
        [FromRoute, Required] long id, PostGardenerProfileDTO updateGardenerProfileDto)
    {
        return await base.PutAsync(id, updateGardenerProfileDto);
    }

    [NonAction]
    public override Task<ActionResult<ServiceResult<GetGardenerProfileDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<PostGardenerProfileDTO> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }

    [NonAction]
    public override Task<ActionResult<ServiceResult<GetGardenerProfileDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }
}
