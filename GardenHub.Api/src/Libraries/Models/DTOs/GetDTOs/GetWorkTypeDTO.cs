using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetWorkTypeDTO
{
    public long Id;

    public string GeneralType { get; set; }
    public string SpecificType { get; set; }

    public List<GetGardenerProfileDTO> Gardeners { get; set; }
}
