using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DbEntities
{
    public class Feedback : EntityBase
    {
        public Project Project { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime EditedDate { get; set; }
        int Rating { get; set; }
        public string Text { get; set; }

        public GardenerProfile Gardener { get; set; }
    }
}
