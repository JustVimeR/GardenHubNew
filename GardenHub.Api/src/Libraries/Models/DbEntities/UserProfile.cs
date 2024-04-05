using System;
using System.Collections.Generic;

namespace Models.DbEntities;

public class UserProfile : EntityBase
{
    public required int IdentityId { get; set; }

    public bool IsGardener { get; set; }

    //CustomerProfile
    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }

    public long? IconId { get; set; }
    public Media? Icon { get; set; }

    public List<Project>? CustomerProjects { get; set; }
    public List<Feedback>? CustomerFeedbacks { get; set; }

    //GardenerProfile
    public string? DescriptionOfExperience { get; set; }

    public List<City>? Cities { get; set; } = new();

    public List<WorkType>? WorkTypes { get; set; }
    public List<Project>? GardenerProjects { get; set; }
    public List<Feedback>? GardenerFeedbacks { get; set; }

 
    public long NotificationChatId { get; set; }
    public Chat? NotificationChat { get; set; }


}