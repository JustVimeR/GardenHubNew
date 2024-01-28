using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DbEntities;
using Models.DTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    public abstract class GenericCRUDDController<T, GetDto, GetSummaryDto, AddDto, PatchDto> : ControllerBase where T : EntityBase where PatchDto : class
    {
        protected readonly IService<T> service;
        protected readonly IMapper mapper;

        protected GenericCRUDDController(IService<T> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public virtual ActionResult<ServiceResult<List<GetSummaryDto>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
        {
            var result = new ServiceResult<List<GetSummaryDto>>();

            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
            var sortFilter = mapper.Map<SortFilter>(sortQuery);
            if (string.IsNullOrEmpty(sortFilter.SortBy))
                sortFilter.SortBy = "Id";
            sortFilter.PropertyInfo = typeof(T).GetProperty(sortFilter.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (sortFilter.PropertyInfo is null)
            {
                result.Successful = false;
                result.Message = "Invalid column for sorting.";
                return BadRequest(result);
            }

            var entities = service.GetAll(paginationFilter, sortFilter);

            result.Data = mapper.Map<List<GetSummaryDto>>(entities);

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public virtual ActionResult<ServiceResult<GetDto>> Get([FromRoute][Required] long id)
        {
            var entity = service.GetFirstOrDefault(item => item.Id == id);

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
        public virtual async Task<ActionResult<ServiceResult<GetDto[]>>> PostAsync(AddDto[] addDtos)
        {
            var result = new ServiceResult<GetDto[]>();

            if (addDtos is null)
            {
                result.Successful = false;
                result.Message = "Argument Null";
                return BadRequest(result);
            }

            var entities = mapper.Map<T[]>(addDtos);

            await service.PostAsync(entities);

            result.Data = mapper.Map<GetDto[]>(entities);

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

            await service.PutAsync(entity);

            result.Data = mapper.Map<GetDto>(entity);

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

}
