using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.Automation
{
    public interface IPluginService
    {
        IEnumerable<PluginDescription> GetPlugins();
        Task UnloadPluginAsync(Guid pluginId);
    }
}
