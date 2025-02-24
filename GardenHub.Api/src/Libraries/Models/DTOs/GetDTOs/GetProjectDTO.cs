﻿using Models.DbEntities;
using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetProjectDTO
{
    public long Id;

    public required string Title { get; set; }
    public string? Location { get; set; }
    public decimal Budget { get; set; }
    public string? Description { get; set; }

    public int NumberOfRequriedGardeners { get; set; }

    public ProjectStatus Status { get; set; }
    public bool IsVerified { get; set; }

    public DateTimeOffset PublicationDate { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public List<GetWorkTypeDTO> WorkTypes { get; set; } = new();
    public List<GetMediaDTO> Medias { get; set; } = new();
    public List<GetUserMiniProfile>? Gardeners { get; set; }

    public long CustomerId { get; set; }
    public string? CustomerName { get; set; }
}
