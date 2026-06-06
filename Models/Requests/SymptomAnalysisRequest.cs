namespace MediAgent.Api.Models.Requests;

public class SymptomAnalysisRequest
{
    public string Description { get; set; } = string.Empty;
    public Guid? ConversationId { get; set; }
}
//symptom analysis request model