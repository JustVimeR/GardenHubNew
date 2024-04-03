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
public class WorkTypeController : BaseCRUDController<WorkType, GetWorkTypeDTO, PostWorkTypeDTO, PostWorkTypeDTO>
{
    ILogger<WorkTypeController> _logger;
    public WorkTypeController(
    IWorkTypeService service,
    IMapper mapper, FilterService filterService,
    ILogger<WorkTypeController> logger)
    : base(service, mapper, filterService)
    {
        _logger = logger;
    }

    public override async Task<ActionResult<ServiceResult<List<GetWorkTypeDTO>>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        return await base.Get(paginationQuery, sortQuery);
    }

    public override async Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> Get([FromRoute, Required] long id)
    {
        return await base.Get(id);
    }

    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> PutAsync([FromRoute, Required] long id, PostWorkTypeDTO addDto)
    {
        return base.PutAsync(id, addDto);
    }

    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }

    [NonAction]
    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<PostWorkTypeDTO> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }
}
