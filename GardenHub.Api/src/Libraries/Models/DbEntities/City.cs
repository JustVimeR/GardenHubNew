using System.Collections.Generic;

namespace Models.DbEntities;

public class City : EntityBase
{
    public required string Name { get; set; }

    public List<UserProfile> Gardeners { get; set; } = new();
}
