using System;
using System.Collections.Generic;

namespace Models.DTOs.PostDTOs;

public class PostUserProfileDTO
{
    public bool IsGardener { get; set; }

    //Customer

    public PostMediaDTO? Icon { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }
}

public class PostGardenerProfileDTO : PostUserProfileDTO
{
    //Gardener

    public string? DescriptionOfExperience { get; set; }

    public List<PostCityDTO>? Cities { get; set; }

    public List<PostIdDTO>? WorkTypes { get; set; }
}