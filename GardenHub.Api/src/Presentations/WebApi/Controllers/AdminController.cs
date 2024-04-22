using AutoMapper;
using Core.Constants;
using Data.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Account;
using Models.Enums;
using Models.ResponseModels;
using Services.IdentityServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    public AdminController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }


    [Authorize(Roles = nameof(Roles.Moderator))]
    [HttpGet("alluser")]
    public async Task<IActionResult> GetAllUser()
    {
        var userList = await _accountService.GetUsers();
        var data = _mapper
            .Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserDto>>(userList);

        return Ok(new BaseResponse<IReadOnlyList<UserDto>>(data, $"User List"));
    }

    [Authorize(Roles = nameof(Roles.Moderator))]
    [HttpGet("alluserwithroles")]
    public async Task<IActionResult> GetAllUserWithRoles()
    {
        var userList = await _accountService.GetUsers();

        var result = userList.Select(x => new UserDto
        {
            Email = x.Email,
            UserName = x.UserName,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Roles = x.UserRoles.ToList().Select(y => y.Role.Name.ToString()).ToList()
        });

        return Ok(new BaseResponse<IEnumerable<UserDto>>(result, $"User List"));
    }
}