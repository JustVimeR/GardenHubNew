using System;

namespace Models.DTOs.GetDTOs;

public class GetUserProfileDTO
{
    public long Id;

    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }

    public long GardenerProfileId { get; set; }
    public long CustomerProfileId { get; set; }
    public long IconId { get; set; }


    public GetMediaDTO? Icon { get; set; }
    public GetGardenerProfileDTO? GardenerProfile { get; set; }
    public GetCustomerProfileDTO? CustomerProfile { get; set; }
}

