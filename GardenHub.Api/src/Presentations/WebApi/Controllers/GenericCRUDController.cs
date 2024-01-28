
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
//using Microsoft.AspNetCore.Mvc;
//using Models.DTOs.Account;
//using Services.IdentityServices.Interfaces;
//using Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace WebApi.Controllers
//{

//    [Authorize]
//    public abstract class GenericCRUDDController<T, GetDto, GetSummaryDto, AddDto, PatchDto>
//        : Abstractions.GenericCRUDDController<T, GetDto, GetSummaryDto, AddDto, PatchDto> where T : OrganizationEntityBase where PatchDto : class
//    {
//        protected IService<Organization> organizationService;
//        protected IAdminAccessor adminAccessor;
//        protected GenericCRUDDController(
//            IService<Organization> organizationService,
//            IAdminAccessor adminAccessor,
//            IService<T> service,
//            IMapper mapper)
//            : base(service, mapper)
//        {
//            this.organizationService = organizationService;
//            this.adminAccessor = adminAccessor;
//        }

//        [HttpGet]
//        public override ActionResult<ServiceResult<List<GetSummaryDto>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
//        {
//            var result = new ServiceResult<List<GetSummaryDto>>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            var paginationFilter = mapper.Map<PaginationFilter>(paginationQuery);
//            var sortFilter = mapper.Map<SortFilter>(sortQuery);

//            if (string.IsNullOrEmpty(sortFilter.SortBy))
//                sortFilter.SortBy = "Id";

//            sortFilter.PropertyInfo = typeof(T).GetProperty(sortFilter.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);


//            if (sortFilter.PropertyInfo is null)
//            {
//                result.Successful = false;
//                result.Message = "Invalid column for sorting.";
//                return BadRequest(result);
//            }

//            var entities = service.GetWhere(entity => entity.Organization!.Id == adminAccessor.OrganizationId, paginationFilter, sortFilter);

//            result.Data = mapper.Map<List<GetSummaryDto>>(entities);

//            return Ok(result);
//        }

//        [HttpGet("{id:long}")]
//        public override ActionResult<ServiceResult<GetDto>> Get([FromRoute][Required] long id)
//        {
//            var result = new ServiceResult<GetDto>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            var entity = service.GetFirstOrDefault(item => item.Id == id && item.Organization!.Id == adminAccessor.OrganizationId);


//            if (entity is null)
//            {
//                result.Successful = false;
//                return NotFound(result);
//            }

//            result.Data = mapper.Map<GetDto>(entity);

//            return Ok(result);
//        }

//        [HttpPost]
//        public override async Task<ActionResult<ServiceResult<GetDto[]>>> PostAsync(AddDto[] addDto)
//        {
//            var result = new ServiceResult<GetDto[]>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            if (addDto is null)
//            {
//                result.Successful = false;
//                result.Message = "Argument Null";
//                return BadRequest(result);
//            }

//            var entities = mapper.Map<T[]>(addDto);
//            if (entities.Length > 0)
//            {
//                foreach (var entity in entities)
//                {
//                    entity.Organization = adminAccessor.Organization;
//                    entity.OrganizationId = adminAccessor.OrganizationId;
//                }
//            }
//            else
//            {
//                result.Successful = false;
//                result.Message = "Argument Null";
//                return BadRequest(result);
//            }


//            await service.PostAsync(entities);

//            result.Data = mapper.Map<GetDto[]>(entities);

//            return Ok(result);
//        }

//        [HttpPut("{id:long}")]
//        public override async Task<ActionResult<ServiceResult<GetDto>>> PutAsync([FromRoute][Required] long id, PatchDto addDto)
//        {
//            var result = new ServiceResult<GetDto>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            if (addDto is null)
//            {
//                result.Successful = false;
//                result.Message = "Argument Null";
//                return BadRequest(result);
//            }

//            var entity = await service.GetFirstOrDefaultAsync(item => item.Id == id && item.OrganizationId == adminAccessor.OrganizationId);

//            if (entity is null)
//            {
//                result.Successful = false;
//                result.Message = "Argument Null";
//                return BadRequest(result);
//            }
//            mapper.Map(addDto, entity);

//            await service.PutAsync(entity);

//            result.Data = mapper.Map<GetDto>(entity);

//            return Ok(result);
//        }

//        [HttpDelete("{id:long}")]
//        public override async Task<ActionResult<ServiceResult<GetDto>>> DeleteAsync([FromRoute][Required] long id, [FromQuery] bool softDelete = false)
//        {
//            var result = new ServiceResult<GetDto>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            var entity = service.GetFirstOrDefault(item => item.Id == id && item.Organization!.Id == adminAccessor.OrganizationId);

//            if (entity is null)
//            {
//                result.Successful = false;
//                return BadRequest(result);
//            }

//            await service.DeleteAsync(entity);

//            result.Data = mapper.Map<GetDto>(entity);
//            return Ok(result);
//        }

//        [HttpPatch("{id:long}")]
//        public override async Task<ActionResult<ServiceResult<GetDto>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<PatchDto> patchDocument)
//        {
//            var result = new ServiceResult<GetDto>();

//            if (!adminAccessor.IsAdmin())
//            {
//                result.Successful = false;
//                return Unauthorized(result);
//            }

//            if (patchDocument is null)
//            {
//                result.Successful = false;
//                result.Message = "Argument Null";
//                return BadRequest(result);
//            }

//            var entity = service.GetFirstOrDefault(item => item.Id == id && item.Organization!.Id == adminAccessor.OrganizationId);

//            if (entity is null)
//            {
//                result.Successful = false;
//                return BadRequest(result);
//            }

//            var patchDto = mapper.Map<PatchDto>(entity);

//            patchDocument.ApplyTo(patchDto, ModelState);

//            if (!ModelState.IsValid)
//            {
//                result.Successful = false;
//                result.Message = "Incorrect model";
//                return BadRequest(result);
//            }

//            mapper.Map(patchDto, entity);

//            await service.PatchAsync(entity);
//            result.Data = mapper.Map<GetDto>(entity);
//            return Ok(result);
//        }
//    }
//}
