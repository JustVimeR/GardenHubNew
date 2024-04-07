using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DbEntities;
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
    private readonly IMapper _mapper;
    private readonly IChatService _chatService;
    private readonly FilterService _filterService;

    public MessageController(
    IChatService chatService,
    FilterService filterService, IMapper mapper)
    {
        _mapper = mapper;
        _chatService = chatService;
        _filterService = filterService;
    }

    [HttpGet("chats")]
    public async Task<ActionResult<ServiceResult<List<GetMiniChatDTO>>>> GetUserChats()
    {
        return Ok(new ServiceResult<List<GetMiniChatDTO>>(await _chatService.GetUserChats()));
    }

    [HttpGet("chats/{chatId:long}")]
    public async Task<ActionResult<ServiceResult<List<GetChatDTO>>>> GetUserChat(long chatId)
    {
        Chat chat = await _chatService.GetUserChat(chatId);

        return Ok(new ServiceResult<GetChatDTO>(_mapper.Map<GetChatDTO>(chat)));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult<ServiceResult<List<GetChatMessageDTO>>>> GetUserNotifications()
    {
        return Ok(new ServiceResult<List<GetChatMessageDTO>>(await _chatService.GetUserNotifications()));
    }
}