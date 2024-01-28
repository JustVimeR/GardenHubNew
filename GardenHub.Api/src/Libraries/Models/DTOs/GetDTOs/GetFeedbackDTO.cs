using System;

namespace Models.DTOs.GetDTOs;

public class GetFeedbackDTO 
{
    public long Id;

    public GetProjectDTO Project { get; set; }
    public DateTime PublicationDate { get; set; }
    public DateTime EditedDate { get; set; }
    int Rating { get; set; }
    public string Text { get; set; }

    public GetGardenerProfileDTO Gardener { get; set; }
}

