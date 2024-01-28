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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : GenericCRUDDController<Feedback, GetFeedbackDTO, GetFeedbackDTO, PostFeedbackDTO,Feedback>
    {
        ILogger<FeedbackController> _logger;
        public FeedbackController(
        IFeedbackService service,
        IMapper mapper,
        ILogger<FeedbackController> logger)
        : base(service, mapper)
        {
            _logger = logger;
        }

        public override ActionResult<ServiceResult<List<GetFeedbackDTO>>> Get([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortQuery sortQuery)
        {
            return base.Get(paginationQuery, sortQuery);
        }

        
        public override ActionResult<ServiceResult<GetFeedbackDTO>> Get([FromRoute, Required] long id)
        {
            return base.Get(id);
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
}

