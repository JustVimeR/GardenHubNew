using System.Collections.Generic;

namespace Models.DbEntities
{
    public class Chat : EntityBase
    {
        public string? Name { get; set; }

        public List<ChatMessage>? ChatMessages { get; set; }

        public long? NotificationOwnerId { get; set; }
        public UserProfile? NotificationOwner { get; set; }

        public long? User1Id { get; set; }
        public UserProfile? User1 { get; set; }

        public long? User2Id { get; set; }
        public UserProfile? User2 { get; set; }
    }
}
