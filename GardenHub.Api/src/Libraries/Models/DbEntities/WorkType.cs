﻿using System.Collections.Generic;

namespace Models.DbEntities;

public class WorkType : EntityBase
{
    public long? ParentWorkTypeId { get; set; }

    public WorkType? ParentWorkType { get; set; }

    public required string Label { get; set; }

    public List<WorkType> DerivedWorkTypes { get; set; } = new();

    public List<Project> Projects { get; set; } = new();

    public List<UserProfile> Gardeners { get; set; } = new();
}
