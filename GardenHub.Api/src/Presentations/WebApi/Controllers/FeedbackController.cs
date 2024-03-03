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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedbackController : BaseCRUDController<Feedback, GetFeedbackDTO, PostFeedbackDTO, Feedback>
{
    ILogger<FeedbackController> _logger;
    public FeedbackController(
    IFeedbackService service,
    IMapper mapper, FilterService filterService,
    ILogger<FeedbackController> logger)
    : base(service, mapper, filterService)
    {
        _logger = logger;
    }

    public override async Task<ActionResult<ServiceResult<List<GetFeedbackDTO>>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        return await base.Get(paginationQuery, sortQuery);
    }


    public override async Task<ActionResult<ServiceResult<GetFeedbackDTO>>> Get([FromRoute, Required] long id)
    {
        return await base.Get(id);
    }


    public override Task<ActionResult<ServiceResult<GetFeedbackDTO>>> PutAsync([FromRoute, Required] long id, Feedback addDto)
    {
        return base.PutAsync(id, addDto);
    }


    public override Task<ActionResult<ServiceResult<GetFeedbackDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
    {
        return base.DeleteAsync(id, softDelete);
    }


    public override Task<ActionResult<ServiceResult<GetFeedbackDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<Feedback> patchDocument)
    {
        return base.PatchAsync(id, patchDocument);
    }
}
