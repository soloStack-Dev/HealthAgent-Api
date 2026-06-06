using System.ComponentModel;
using Microsoft.Extensions.AI;

namespace MediAgent.Api.Agents.Plugins;

public class SymptomAnalyzerPlugin
{
    [Description("Analyzes user-described symptoms and provides possible conditions with confidence levels")]
    public string AnalyzeSymptoms(
        [Description("The symptoms described by the user")] string symptoms)
    {
        return $"Analyzing symptoms: {symptoms}";
    }
}
