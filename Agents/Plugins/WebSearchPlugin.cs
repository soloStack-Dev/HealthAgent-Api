using System.ComponentModel;
using Microsoft.Extensions.AI;

namespace MediAgent.Api.Agents.Plugins;

public class WebSearchPlugin
{
    [Description("Searches for verified medical resources related to a condition")]
    public string SearchMedicalResources(
        [Description("The medical condition to search for")] string condition)
    {
        return $"Searching verified medical resources for: {condition}";
    }
}
