using System;

namespace Models.DbEntities;

public class UserProfile : EntityBase
{
    public required int IdentityId { get; set; }

    public required string Name { get; set; }
    public string? Surname { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }

    public DateOnly BirthDate { get; set; }

    public long? IconId { get; set; }
    public Media? Icon { get; set; }

    public long CustomerProfileId { get; set; }
    public long? GardenerProfileId { get; set; }

    public CustomerProfile? CustomerProfile { get; set; }
    public GardenerProfile? GardenerProfile { get; set; }
}