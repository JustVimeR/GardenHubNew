using System;
using System.Collections.Generic;
namespace Models.DbEntities
{
    public class Project: EntityBase
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Budget { get; set; }
        public string Description { get; set; }

        public int NumberOfRequriedGardeners { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsVerified { get; set; }


        public DateTime PublicationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public List<WorkType> WorkTypes { get; set; }
        public List<Media> Medias { get; set; }
        public CustomerProfile Customer { get; set; }
        public List<GardenerProfile> Gardeners { get; set; }
    }
}
