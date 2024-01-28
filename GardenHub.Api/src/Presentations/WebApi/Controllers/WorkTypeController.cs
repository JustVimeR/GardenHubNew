using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
using Models.DTOs;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkTypeController : GenericCRUDDController<WorkType, GetWorkTypeDTO, GetWorkTypeDTO, PostWorkTypeDTO, WorkType>
{
    ILogger<WorkTypeController> _logger;
    public WorkTypeController(
    IWorkTypeService service,
    IMapper mapper,
    ILogger<WorkTypeController> logger)
    : base(service, mapper)
    {
        _logger = logger;
    }

    public override ActionResult<ServiceResult<List<GetWorkTypeDTO>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        return base.Get(paginationQuery, sortQuery);
    }

    public override ActionResult<ServiceResult<GetWorkTypeDTO>> Get([FromRoute, Required] long id)
    {
        return base.Get(id);
    }


    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> PutAsync([FromRoute, Required] long id, WorkType addDto)
    {
        return base.PutAsync(id, addDto);
    }


    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }


    public override Task<ActionResult<ServiceResult<GetWorkTypeDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<WorkType> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }
}
