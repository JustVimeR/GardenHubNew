﻿using System;

namespace Models.DTOs.PostDTOs;

public class PostUserProfileDTO
{
    //public PostMediaDTO? Icon { get; set; }
    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }
}
