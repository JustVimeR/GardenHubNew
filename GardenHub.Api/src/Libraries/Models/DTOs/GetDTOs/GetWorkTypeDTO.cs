using Models.DbEntities;
using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetWorkTypeDTO : EntityBase
{
    public long? ParentWorkTypeId { get; set; }

    public WorkType? ParentWorkType { get; set; }

    public required string Label { get; set; }

    public List<WorkType> DerivedWorkTypes { get; set; } = new();

    public List<GetGardenerProfileDTO> GardenerProfiles { get; set; } = new();
}