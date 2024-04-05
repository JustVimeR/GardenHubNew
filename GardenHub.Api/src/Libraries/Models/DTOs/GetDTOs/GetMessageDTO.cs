using System;

namespace Models.DTOs.GetDTOs;

public class GetChatMessageDTO
{
    public long Id { get; set; }
    public long ChatId { get; set; }

    public long? SenderUserId { get; set; }

    public DateTimeOffset PublicationDate { get; set; }
    public string? Message { get; set; }
}