namespace MediAgent.Api.Common;

public static class Constants
{
    public const string MedicalDisclaimer = "This is not a medical diagnosis. Please consult a qualified healthcare professional.";

    public const string EmergencyMessage = "⚠️ If you are experiencing a medical emergency (chest pain, difficulty breathing, severe bleeding, loss of consciousness), please call emergency services (911 / local emergency number) immediately.";

    public static class Roles
    {
        public const string User = "User";
        public const string Assistant = "Assistant";
        public const string System = "System";
        public const string Tool = "Tool";
    }

    public static class UrgencyLevels
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
        public const string Emergency = "Emergency";
    }
}
