using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTOs.GetDTOs;
using Services;
using Services.GardenhubServices.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly FilterService _filterService;

    public MessageController(
    IChatService chatService,
    FilterService filterService)
    {
        _chatService = chatService;
        _filterService = filterService;
    }

    [HttpGet("chats")]
    public async Task<ActionResult<ServiceResult<List<GetChatDTO>>>> GetUserChats()
    {
        return Ok(new ServiceResult<List<GetChatDTO>>(await _chatService.GetUserChats()));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult<ServiceResult<List<GetChatMessageDTO>>>> GetUserNotifications()
    {
        return Ok(new ServiceResult<List<GetChatMessageDTO>>(await _chatService.GetUserNotifications()));
    }
}