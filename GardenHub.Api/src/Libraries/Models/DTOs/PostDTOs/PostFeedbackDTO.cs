using System;

namespace Models.DTOs.PostDTOs;

public class PostFeedbackDTO
{
    public DateTime PublicationDate { get; set; }
    public DateTime EditedDate { get; set; }
    public long Rating { get; set; }
    public string? Text { get; set; }

    public long GardenerId { get; set; }
    public long ProjectId { get; set; }

    public PostProjectDTO? Project { get; set; }
    public PostGardenerProfileDTO? Gardener { get; set; }
}
