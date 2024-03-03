using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DbEntities;
using Services;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[ApiController]
public abstract class BaseCRUDController<T, GetDto, AddDto, PatchDto> : ControllerBase where T : IEntityBase where PatchDto : class
{
    protected readonly IService<T> service;
    protected readonly IMapper mapper;
    protected readonly FilterService _filterService;

    protected BaseCRUDController(IService<T> service, IMapper mapper, FilterService filterService)
    {
        this.service = service;
        this.mapper = mapper;
        _filterService = filterService;
    }

    [HttpGet]
    public virtual async Task<ActionResult<ServiceResult<List<GetDto>>>> Get(
        [FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
    {
        var result = new ServiceResult<List<GetDto>>();

        var serviceResult = _filterService.GetFilters<T>(paginationQuery, sortQuery);

        if (!serviceResult.Successful)
        {
            result.Successful = false;
            result.Message = serviceResult.Message;

            return BadRequest(result);
        }

        var entities = await service.GetAllAsync(serviceResult.Data.PaginationFilter, serviceResult.Data.SortFilter);

        result.Data = mapper.Map<List<GetDto>>(entities);

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public virtual async Task<ActionResult<ServiceResult<GetDto>>> Get([FromRoute][Required] long id)
    {
        var entity = await service.GetFirstOrDefaultAsync(item => item.Id == id);

        var result = new ServiceResult<GetDto>();

        if (entity is null)
        {
            result.Successful = false;
            return NotFound(result);
        }

        result.Data = mapper.Map<GetDto>(entity);

        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<ServiceResult<GetDto>>> PostAsync(AddDto addDto)
    {
        var result = new ServiceResult<GetDto>();

        if (addDto is null)
        {
            result.Successful = false;
            result.Message = "Argument Null";
            return BadRequest(result);
        }

        var entity = mapper.Map<T>(addDto);

        await service.PostAsync(entity);

        result.Data = mapper.Map<GetDto>(entity);

        return Ok(result);
    }

    [HttpPut("{id:long}")]
    public virtual async Task<ActionResult<ServiceResult<GetDto>>> PutAsync([FromRoute][Required] long id, PatchDto addDto)
    {
        var result = new ServiceResult<GetDto>();

        if (addDto is null)
        {
            result.Successful = false;
            result.Message = "Argument Null";
            return BadRequest(result);
        }

        var entity = mapper.Map<T>(addDto);
        entity.Id = id;

        result.Data = mapper.Map<GetDto>(await service.PutAsync(entity));

        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    public virtual async Task<ActionResult<ServiceResult<GetDto>>> DeleteAsync([FromRoute][Required] long id, [FromQuery] bool softDelete = false)
    {
        var result = new ServiceResult<GetDto>();

        var entity = service.GetFirstOrDefault(item => item.Id == id);

        if (entity is null)
        {
            result.Successful = false;
            return NotFound(result);
        }

        await service.DeleteAsync(entity);
        return Ok(result);
    }

    [HttpPatch("{id:long}")]
    public virtual async Task<ActionResult<ServiceResult<GetDto>>> PatchAsync([FromRoute][Required] long id, [FromBody] JsonPatchDocument<PatchDto> patchDocument)
    {
        var result = new ServiceResult<GetDto>();

        var entity = service.GetFirstOrDefault(item => item.Id == id);

        if (entity is null)
        {
            result.Successful = false;
            return NotFound(result);
        }

        var patchDto = mapper.Map<PatchDto>(entity);

        patchDocument.ApplyTo(patchDto, ModelState);

        if (!ModelState.IsValid)
        {
            result.Successful = false;
            result.Message = "Incorrect model";
            return BadRequest(result);
        }

        await service.PatchAsync(entity);
        result.Data = mapper.Map<GetDto>(entity);
        return Ok(result);
    }
}
