﻿using System;

namespace Models.DbEntities
{
    public class Feedback : EntityBase
    {
        public DateTime PublicationDate { get; set; }
        public DateTime EditedDate { get; set; }
        public int Rating { get; set; }
        public string? Text { get; set; }

        public long GardenerId { get; set; }
        public long? CustomerId { get; set; }
        public long? ProjectId { get; set; }

        public Project? Project { get; set; }
        public UserProfile? Customer { get; set; }
        public UserProfile? Gardener { get; set; }
    }
}
