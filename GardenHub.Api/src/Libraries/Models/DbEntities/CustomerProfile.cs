using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class CustomerProfile: EntityBase
    {
        public List<Project> Projects { get; set; }
    }
}
