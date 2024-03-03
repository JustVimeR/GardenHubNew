using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : BaseCRUDController<City, GetCityDTO, PostCityDTO, PostCityDTO>
{
    ILogger<CityController> _logger;
    public CityController(
    ICityService service,
    IMapper mapper,
    FilterService filterService,
    ILogger<CityController> logger)
    : base(service, mapper, filterService)
    {
        _logger = logger;
    }

    public override async Task<ActionResult<ServiceResult<List<GetCityDTO>>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        return await base.Get(paginationQuery, sortQuery);
    }


    public override async Task<ActionResult<ServiceResult<GetCityDTO>>> Get([FromRoute, Required] long id)
    {
        return await base.Get(id);
    }


    public override Task<ActionResult<ServiceResult<GetCityDTO>>> PutAsync([FromRoute, Required] long id, PostCityDTO addDto)
    {
        return base.PutAsync(id, addDto);
    }


    public override Task<ActionResult<ServiceResult<GetCityDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }


    public override Task<ActionResult<ServiceResult<GetCityDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<PostCityDTO> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }
}