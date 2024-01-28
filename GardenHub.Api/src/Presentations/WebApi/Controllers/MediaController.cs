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

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : GenericCRUDDController<Media, GetMediaDTO, GetMediaDTO, PostMediaDTO,Media>
    {
        ILogger<MediaController> _logger;
        public MediaController(
        IMediaService service,
        IMapper mapper,
        ILogger<MediaController> logger)
        : base(service, mapper)
        {
            _logger = logger;
        }

        public override ActionResult<ServiceResult<List<GetMediaDTO>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
        {
            return base.Get(paginationQuery, sortQuery);
        }

        
        public override ActionResult<ServiceResult<GetMediaDTO>> Get([FromRoute, Required] long id)
        {
            return base.Get(id);
        }

        
        public override Task<ActionResult<ServiceResult<GetMediaDTO>>> PutAsync([FromRoute, Required] long id, Media addDto)
        {
            return base.PutAsync(id, addDto);
        }

        
        public override Task<ActionResult<ServiceResult<GetMediaDTO>>> DeleteAsync([FromRoute, Required] long id, [FromQuery] bool softDelete = false)
        {
            return base.DeleteAsync(id, softDelete);
        }

        
        public override Task<ActionResult<ServiceResult<GetMediaDTO>>> PatchAsync([FromRoute, Required] long id, [FromBody] JsonPatchDocument<Media> patchDocument)
        {
            return base.PatchAsync(id, patchDocument);
        }
    }
}

