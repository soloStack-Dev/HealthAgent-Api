namespace MediAgent.Api.Models.Responses;

public class AgentResponse
{
    public string Content { get; set; } = string.Empty;
    public string ModelUsed { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; }
}
//agent response model