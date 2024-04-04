using LagDaemon.YAMUD.Model.User;

namespace LagDaemon.YAMUD.Model.Utilities
{
    public class Annotation
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; } // Assuming string for simplicity, replace with appropriate user identifier type
        public UserAccount User { get; set; } // Assuming ApplicationUser is your user class, replace with your actual user class

        // Foreign key for the annotated entity
        public Guid AnnotatedEntityId { get; set; }
        public string AnnotatedEntityName { get; set; } // Name of the annotated entity type (e.g., "Character")
    }
}
