using System;

namespace Models.DTOs.GetDTOs;

public class GetUserProfileDTO
{
    public long Id;

    public GetMediaDTO Icon { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }

    public DateTime BirthDate { get; set; }

    public GetGardenerProfileDTO GardenerProfile { get; set; }
    public GetCustomerProfileDTO CustomerProfile { get; set; }
}

