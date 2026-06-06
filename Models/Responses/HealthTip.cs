namespace MediAgent.Api.Models.Responses;

public class HealthTip
{
    public string Category { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "Recommended";
}


//health tip model