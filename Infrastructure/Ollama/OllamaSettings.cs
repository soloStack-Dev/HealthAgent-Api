namespace MediAgent.Api.Infrastructure.Ollama;

public class OllamaSettings
{
    public string Endpoint { get; set; } = "http://localhost:11434";
    public string Model { get; set; } = "phi3.5:latest";
    public string? FallbackModel { get; set; }
    public int TimeoutSeconds { get; set; } = 120;
    public int MaxTokens { get; set; } = 2048;
    public float Temperature { get; set; } = 0.3f;
}
