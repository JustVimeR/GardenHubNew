using System.Collections.Generic;

namespace Models.DTOs.PostDTOs;

public class PostGardenerProfileDTO
{
    public string? DescriptionOfExperience { get; set; }

    public List<PostCityDTO> Cities { get; set; } = new();

    public List<PostGardenerWorkTypeDTO> WorkTypes { get; set; } = new();
}
