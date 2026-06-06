namespace MediAgent.Api.Models.Responses;

public class ConversationSummary
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string UrgencyLevel { get; set; } = "Low";
    public int MessageCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

//conversation summary model
