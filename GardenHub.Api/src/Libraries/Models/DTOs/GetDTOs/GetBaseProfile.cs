using System;
using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetBaseProfile
{
    public long Id;

    //Customer
    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }

    public long IconId { get; set; }

    public GetMediaDTO? Icon { get; set; }

    public List<GetProjectDTO> CustomerProjects { get; set; } = new();
    public List<GetFeedbackDTO> CustomerFeedbacks { get; set; } = new();
}

