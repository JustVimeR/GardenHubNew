using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetGardenerProfileDTO
{
    public long Id;

    public string? DescriptionOfExperience { get; set; }
    public List<GetCityDTO> Cities { get; set; } = new();

    public List<GetWorkTypeDTO> WorkTypes { get; set; } = new();
    public List<GetProjectDTO> Projects { get; set; } = new();
    public List<GetFeedbackDTO> Feedbacks { get; set; } = new();
}
