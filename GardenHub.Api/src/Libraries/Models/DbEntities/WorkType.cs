using System;
using System.Collections.Generic;

namespace Models.DbEntities
{
    public class WorkType: EntityBase
    {
        public string GeneralType { get; set; }
        public string SpecificType { get; set; }
        
        public List<GardenerProfile> Gardeners { get; set; }
    }
}