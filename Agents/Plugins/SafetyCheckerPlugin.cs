using System.ComponentModel;
using Microsoft.Extensions.AI;

namespace MediAgent.Api.Agents.Plugins;

public class SafetyCheckerPlugin
{
    [Description("Checks if the agent response contains any prohibited content or missing disclaimers")]
    public string CheckResponseSafety(
        [Description("The response content to check")] string responseContent)
    {
        var hasDisclaimer = responseContent.Contains("medical diagnosis", StringComparison.OrdinalIgnoreCase)
            || responseContent.Contains("healthcare professional", StringComparison.OrdinalIgnoreCase);

        if (!hasDisclaimer)
        {
            return "SAFETY_ISSUE: Missing medical disclaimer";
        }

        return "SAFE";
    }
}

/*

// This would FAIL even though it's clearly a disclaimer:
"Please consult your physician or qualified health provider."

// This would PASS even though it's dangerous:
"Here's my medical diagnosis: you're fine. No need to see a healthcare professional."

*/