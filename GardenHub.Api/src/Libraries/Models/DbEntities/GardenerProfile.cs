using System.Collections.Generic;

namespace Models.DbEntities;

public class GardenerProfile : EntityBase
{
    public string? DescriptionOfExperience { get; set; }

    public List<City>? Cities { get; set; } = new();

    public List<WorkType>? WorkTypes { get; set; }
    public List<Project>? Projects { get; set; }
    public List<Feedback>? Feedbacks { get; set; }
}
