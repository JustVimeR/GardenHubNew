using System.Collections.Generic;

namespace Models.DTOs.GetDTOs;

public class GetCustomerProfileDTO
{
    public long Id;

    public List<GetProjectDTO> Projects { get; set; } = new();
}
