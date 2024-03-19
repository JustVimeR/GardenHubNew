using Models.Enums;
using System;
using System.Collections.Generic;

namespace Models.DbEntities;

public class Project : EntityBase
{
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

    public long CustomerId { get; set; }

    public UserProfile? Customer { get; set; }

    public List<WorkType>? WorkTypes { get; set; }
    public List<ProjectMedia>? Medias { get; set; }
    public List<UserProfile>? Gardeners { get; set; }
}

