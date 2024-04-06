using System;

namespace Models.DbEntities;

public class ChatMessage : EntityBase
{
    public long ChatId { get; set; }

    public long? SenderUserId { get; set; }
    public UserProfile? SenderUser { get; set; }

    public DateTimeOffset PublicationDate { get; set; }
    public string? Message { get; set; }
}
