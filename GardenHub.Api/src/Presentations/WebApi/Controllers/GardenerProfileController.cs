using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
using Models.DTOs.GetDTOs;
using Models.DTOs.PostDTOs;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenerProfileController : GenericCRUDDController<GardenerProfile, GetGardenerProfileDTO, GetGardenerProfileDTO, PostGardenerProfileDTO, GardenerProfile>
    {
        ILogger<UserProfileController> _logger;

        public GardenerProfileController(
        IGardenerProfileService service,
        IMapper mapper,
        ILogger<UserProfileController> logger)
        : base(service, mapper)
        {
            _logger = logger;
        }

        [Authorize]
        public override async Task<ActionResult<ServiceResult<GetGardenerProfileDTO[]>>> PostAsync(PostGardenerProfileDTO[] addDtos)
        {
            
            return await base.PostAsync(addDtos);
        }
    }
}

