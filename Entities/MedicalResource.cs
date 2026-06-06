namespace MediAgent.Api.Entities;

public class MedicalResource
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MessageId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal TrustScore { get; set; } = 0.95m;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Message Message { get; set; } = null!;
}

//one message have many medical resources
//one medical resource have one message
