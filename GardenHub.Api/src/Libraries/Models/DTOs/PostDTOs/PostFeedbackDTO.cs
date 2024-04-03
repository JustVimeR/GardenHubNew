namespace Models.DTOs.PostDTOs;

public class PostFeedbackDTO
{
    public long Rating { get; set; }
    public string? Text { get; set; }

    public long GardenerId { get; set; }
    public long ProjectId { get; set; }
}
