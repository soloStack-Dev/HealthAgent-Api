using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediAgent.Api.Infrastructure.Ollama;

public class OllamaClient
{
    private readonly HttpClient _httpClient;
    private readonly string _model;
    private readonly ILogger<OllamaClient>? _logger;

    public OllamaClient(string endpoint, string model, int timeoutSeconds = 120, ILogger<OllamaClient>? logger = null)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(endpoint.TrimEnd('/') + "/"),
            Timeout = TimeSpan.FromSeconds(timeoutSeconds)
        };
        _model = model;
        _logger = logger;
    }

    public async Task<string> ChatAsync(string systemPrompt, string userPrompt, int maxTokens = 2048, float temperature = 0.3f)
    {
        var request = new OllamaChatRequest
        {
            Model = _model,
            Messages = new[]
            {
                new OllamaMessage { Role = "system", Content = systemPrompt },
                new OllamaMessage { Role = "user", Content = userPrompt }
            },
            Options = new OllamaOptions
            {
                Temperature = temperature,
                NumPredict = maxTokens
            },
            Stream = false,
            Format = "json"
        };

        var response = await _httpClient.PostAsJsonAsync("api/chat", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OllamaChatResponse>();

        return result?.Message?.Content ?? string.Empty;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}

public class OllamaChatRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("messages")]
    public OllamaMessage[] Messages { get; set; } = Array.Empty<OllamaMessage>();

    [JsonPropertyName("options")]
    public OllamaOptions? Options { get; set; }

    [JsonPropertyName("stream")]
    public bool Stream { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }
}

public class OllamaMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}

public class OllamaOptions
{
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; }

    [JsonPropertyName("num_predict")]
    public int NumPredict { get; set; }
}

public class OllamaChatResponse
{
    [JsonPropertyName("message")]
    public OllamaMessage? Message { get; set; }

    [JsonPropertyName("done")]
    public bool Done { get; set; }
}
