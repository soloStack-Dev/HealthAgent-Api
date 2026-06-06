namespace MediAgent.Api.Models.Responses;

public class MedicalResourceDto
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Relevance { get; set; } = "Medium";
}


//medical resource model