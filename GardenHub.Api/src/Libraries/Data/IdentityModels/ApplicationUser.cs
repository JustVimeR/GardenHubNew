using Microsoft.AspNetCore.Identity;
using Models.DbEntities;
using System.Collections.Generic;

namespace Data.IdentityModels
{
    public class ApplicationUser : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<ApplicationUserRole>? UserRoles { get; set; }


        public long? UserProfileId { get; set; }
        public UserProfile? UserProfile { get; set; }
    }
}
