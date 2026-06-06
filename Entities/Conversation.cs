namespace MediAgent.Api.Entities;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string UrgencyLevel { get; set; } = "Low";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}

//one user have many conversations with many messages
//one conversation have many messages with many medical resources and one user