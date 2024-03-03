using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetWorkTypeDTO
{
    public long Id { get; set; }

    public required string Label { get; set; }

    public List<GetDerivedWorkTypeDTO> DerivedWorkTypes { get; set; } = new();
}

public class GetDerivedWorkTypeDTO
{
    public long Id { get; set; }

    public long? ParentWorkTypeId { get; set; }

    public required string Label { get; set; }
}