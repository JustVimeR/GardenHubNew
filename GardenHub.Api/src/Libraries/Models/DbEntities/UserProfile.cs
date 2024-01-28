using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class UserProfile : EntityBase
    {
        public int IdentityId { get; set; }
        public Media Icon { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }

        public DateTime BirthDate { get; set; }
        
        public CustomerProfile CustomerProfile { get; set; }
        public GardenerProfile GardenerProfile { get; set; }
    }
}
