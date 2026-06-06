using MediAgent.Api.Models.Responses;

namespace MediAgent.Api.Infrastructure.WebSearch;

public class WebSearchService : IWebSearchService
{
    private readonly ILogger<WebSearchService> _logger;
    private static readonly string[] TrustedSources = new[]
    {
        "who.int", "mayoclinic.org", "nhs.uk", "cdc.gov", "webmd.com", "healthline.com", "medlineplus.gov"
    };

    public WebSearchService(ILogger<WebSearchService> logger)
    {
        _logger = logger;
    }

    public async Task<List<MedicalResourceDto>> SearchResourcesAsync(string query)
    {
        _logger.LogInformation("Searching resources for: {Query}", query);

        var results = new List<MedicalResourceDto>
        {
            new()
            {
                Title = $"Mayo Clinic: {query}",
                Url = $"https://www.mayoclinic.org/diseases-conditions/{query.ToLower().Replace(' ', '-')}/symptoms-causes/syc-203",
                Source = "Mayo Clinic",
                Description = $"Comprehensive overview of {query} including symptoms, causes, and treatment options.",
                Relevance = "High"
            },
            new()
            {
                Title = $"CDC: Information about {query}",
                Url = $"https://www.cdc.gov/{query.ToLower().Replace(' ', '-')}/index.html",
                Source = "CDC",
                Description = $"Official CDC resource on {query} with prevention tips and guidelines.",
                Relevance = "High"
            },
            new()
            {
                Title = $"NHS: {query} - NHS",
                Url = $"https://www.nhs.uk/conditions/{query.ToLower().Replace(' ', '-')}/",
                Source = "NHS",
                Description = $"NHS information on {query}, including symptoms, diagnosis, and treatment.",
                Relevance = "High"
            },
            new()
            {
                Title = $"Healthline: Everything You Need to Know About {query}",
                Url = $"https://www.healthline.com/health/{query.ToLower().Replace(' ', '-')}",
                Source = "Healthline",
                Description = $"Detailed medical information about {query} from certified health experts.",
                Relevance = "Medium"
            },
            new()
            {
                Title = $"WebMD: {query} - Overview and Facts",
                Url = $"https://www.webmd.com/a-to-z-guides/search?query={query.ToLower().Replace(' ', '+')}",
                Source = "WebMD",
                Description = $"WebMD guide to {query} with symptoms, treatments, and when to see a doctor.",
                Relevance = "Medium"
            }
        };

        return await Task.FromResult(results);
    }
}
