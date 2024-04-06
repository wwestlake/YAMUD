using LagDaemon.YAMUD.Model.User;
using System.Text.Json.Serialization;

namespace LagDaemon.YAMUD.Model.Scripting
{
    public enum SupportedCodeLanguages
    {
        Python,
        Ruby
    }

    public class CodeModule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "New Module";
        public string Code { get; set; } = string.Empty;
        public SupportedCodeLanguages Language { get; set; } = SupportedCodeLanguages.Python;
        public string Description { get; set; } = string.Empty;
        public Guid UserAccountId { get; set; }
        public string Version { get; set; } = "1.0.0.0";
        public string License { get; set; } = string.Empty;
    }
}
