using MediAgent.Api.Models.Responses;

namespace MediAgent.Api.Infrastructure.WebSearch;

public interface IWebSearchService
{
    Task<List<MedicalResourceDto>> SearchResourcesAsync(string query);
}
