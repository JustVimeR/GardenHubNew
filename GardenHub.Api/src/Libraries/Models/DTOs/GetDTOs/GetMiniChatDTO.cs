namespace Models.DTOs.GetDTOs;

public class GetMiniChatDTO
{
    public long Id { get; set; }

    public GetChatMessageDTO? LastChatMessage { get; set; }

    public GetUserMiniProfile? InterlocutorProfile { get; set; }
}
