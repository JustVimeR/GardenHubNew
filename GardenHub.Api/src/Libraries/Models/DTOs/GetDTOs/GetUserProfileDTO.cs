using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetUserProfileDTO : GetBaseProfile
{
    //Gardener
    public string? DescriptionOfExperience { get; set; }
    public List<GetCityDTO>? Cities { get; set; } = new();

    public List<GetWorkTypeDTO>? WorkTypes { get; set; }
    public List<GetProjectDTO>? GardenerProjects { get; set; }
    public List<GetFeedbackDTO>? GardenerFeedbacks { get; set; }
}

