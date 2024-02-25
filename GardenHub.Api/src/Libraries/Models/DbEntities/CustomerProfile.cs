using System.Collections.Generic;

namespace Models.DbEntities;

public class CustomerProfile : EntityBase
{
    public List<Project> Projects { get; set; } = new();
}

