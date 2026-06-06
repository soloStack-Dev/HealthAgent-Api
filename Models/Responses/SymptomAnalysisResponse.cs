namespace MediAgent.Api.Models.Responses;

public class SymptomAnalysisResponse
{
    public Guid ConversationId { get; set; }
    public ParsedAgentResponse? Analysis { get; set; }
    public List<string> HealthTips { get; set; } = new();
    public List<MedicalResourceDto> Resources { get; set; } = new();
    public string UrgencyLevel { get; set; } = "Low";
    public string Disclaimer { get; set; } = "This is not a medical diagnosis. Please consult a qualified healthcare professional.";
    public string? ModelUsed { get; set; }
    public int ProcessingTimeMs { get; set; }
}

public class ParsedAgentResponse
{
    public List<PossibleCondition>? PossibleConditions { get; set; }
    public string? UrgencyLevel { get; set; }
    public List<string>? RecommendedActions { get; set; }
    public List<string>? QuestionsToAsk { get; set; }
    public string? RawContent { get; set; }
}

public class PossibleCondition
{
    public string Name { get; set; } = string.Empty;
    public string Confidence { get; set; } = "Low";
    public string Description { get; set; } = string.Empty;
    public List<string> MatchingSymptoms { get; set; } = new();
}

//symptom analysis response model