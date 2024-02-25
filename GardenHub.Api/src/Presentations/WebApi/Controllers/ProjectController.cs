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
public class ProjectController : GenericCRUDDController<Project, GetProjectDTO, PostProjectDTO, Project>
{
    ILogger<ProjectController> _logger;
    public ProjectController(
    IProjectService service,
    IMapper mapper, FilterService filterService,
    ILogger<ProjectController> logger)
    : base(service, mapper, filterService)
    {
        _logger = logger;
    }

    public override async Task<ActionResult<ServiceResult<List<GetProjectDTO>>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        return await base.Get(paginationQuery, sortQuery);
    }


    public override async Task<ActionResult<ServiceResult<GetProjectDTO>>> Get([FromRoute, Required] long id)
    {
        return await base.Get(id);
    }


    public override Task<ActionResult<ServiceResult<GetProjectDTO>>> PutAsync([FromRoute, Required] long id, Project addDto)
    {
        return base.PutAsync(id, addDto);
    }


    public override Task<ActionResult<ServiceResult<GetProjectDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }


    public override Task<ActionResult<ServiceResult<GetProjectDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<Project> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }
}