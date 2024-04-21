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
    private readonly IUserAccessor _userAccessor;
    private readonly FilterService _filterService;

    public MessageController(
    IChatService chatService,
    FilterService filterService, IMapper mapper, IUserAccessor userAccessor)
    {
        _mapper = mapper;
        _chatService = chatService;
        _filterService = filterService;
        _userAccessor = userAccessor;
    }

    [HttpGet("chats")]
    public async Task<ActionResult<ServiceResult<List<GetMiniChatDTO>>>> GetUserChats()
    {
        long userId = _userAccessor.UserProfileId;

        return Ok(new ServiceResult<List<GetMiniChatDTO>>(await _chatService.GetUserChats(userId)));
    }

    [HttpGet("chats/{chatId:long}")]
    public async Task<ActionResult<ServiceResult<List<GetChatDTO>>>> GetUserChat(long chatId)
    {
        long userId = _userAccessor.UserProfileId;

        return Ok(new ServiceResult<GetChatDTO>(await _chatService.GetUserChat(chatId, userId)));
    }

    [HttpGet("notifications")]
    public async Task<ActionResult<ServiceResult<List<GetChatMessageDTO>>>> GetUserNotifications()
    {
        long userId = _userAccessor.UserProfileId;

        return Ok(new ServiceResult<List<GetChatMessageDTO>>(_mapper.Map<List<GetChatMessageDTO>>(
            await _chatService.GetUserNotifications(userId))));
    }

    [HttpDelete("notifications/{messageId:long}")]
    public async Task<ActionResult<ServiceResult<bool>>> DeleteUserNotification(long messageId)
    {
        long userId = _userAccessor.UserProfileId;

        await _chatService.DeleteMessage(messageId, userId);

        return Ok(new ServiceResult<bool>());
    }
}