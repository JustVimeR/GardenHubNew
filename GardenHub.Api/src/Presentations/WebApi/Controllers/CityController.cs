using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
using Models.DTOs;
using Models.DTOs.GetDTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : GenericCRUDDController<City, GetCityDTO, GetCityDTO, City,City>
    {
        ILogger<CityController> _logger;
        public CityController(
        ICityService service,
        IMapper mapper,
        ILogger<CityController> logger)
        : base(service, mapper)
        {
            _logger = logger;
        }

        public override ActionResult<ServiceResult<List<GetCityDTO>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
        {
            return base.Get(paginationQuery, sortQuery);
        }

        
        public override ActionResult<ServiceResult<GetCityDTO>> Get([FromRoute, Required] long id)
        {
            return base.Get(id);
        }

        
        public override Task<ActionResult<ServiceResult<GetCityDTO>>> PutAsync([FromRoute, Required] long id, City addDto)
        {
            return base.PutAsync(id, addDto);
        }

        
        public override Task<ActionResult<ServiceResult<GetCityDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
        {
            return base.DeleteAsync(id, softDelete);
        }

        
        public override Task<ActionResult<ServiceResult<GetCityDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<City> patchDocument)
        {
            return base.PatchAsync(id, patchDocument);
        }
    }
}

