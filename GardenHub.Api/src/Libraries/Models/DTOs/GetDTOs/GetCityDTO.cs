using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetCityDTO
{
    public long Id;

    public required string Name { get; set; }

    public List<GetGardenerProfileDTO> Gardeners { get; set; } = new();
}
