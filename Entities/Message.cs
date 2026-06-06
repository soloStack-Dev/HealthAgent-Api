namespace MediAgent.Api.Entities;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? PossibleConditions { get; set; }
    public string? HealthTips { get; set; }
    public string? ResourceLinks { get; set; }
    public string? SafetyFlags { get; set; }
    public string? ModelUsed { get; set; }
    public int? TokensUsed { get; set; }
    public int? ProcessingTimeMs { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Conversation Conversation { get; set; } = null!;
    public ICollection<MedicalResource> MedicalResources { get; set; } = new List<MedicalResource>();
}


//one conversation have many messages and many medical resources