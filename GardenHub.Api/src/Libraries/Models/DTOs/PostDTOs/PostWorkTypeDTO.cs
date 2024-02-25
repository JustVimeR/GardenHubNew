using System.Collections.Generic;

namespace Models.DTOs.PostDTOs;

public class PostWorkTypeDTO
{
    public long? ParentWorkTypeId { get; set; }

    public string? Label { get; set; }

    public List<PostDerivedWorkTypeDTO>? DerivedWorkTypes { get; set; }
}

public class PostDerivedWorkTypeDTO
{
    public long? ParentWorkTypeId { get; set; }

    public string? Label { get; set; }
}