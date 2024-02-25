using System;

namespace Models.DTOs.GetDTOs;

public class GetFeedbackDTO 
{
    public long Id;

    public DateTime PublicationDate { get; set; }
    public DateTime EditedDate { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }

    public long GardenerId { get; set; }
    public long ProjectId { get; set; }

    public GetProjectDTO? Project { get; set; }
    public GetGardenerProfileDTO? Gardener { get; set; }
}