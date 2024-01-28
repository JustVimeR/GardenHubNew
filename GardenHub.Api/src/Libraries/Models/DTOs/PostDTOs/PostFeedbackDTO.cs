using Models.DbEntities;
using System;

namespace Models.DTOs.PostDTOs
{
    public class PostFeedbackDTO
    {
        public Project Project { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime EditedDate { get; set; }
        int Rating { get; set; }
        public string Text { get; set; }

        public PostGardenerProfileDTO Gardener { get; set; }
    }
}
