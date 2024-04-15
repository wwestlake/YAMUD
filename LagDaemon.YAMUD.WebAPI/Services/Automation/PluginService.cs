using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.Automation;
using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Model.Automation
{
    public class PluginService : IPluginService
    {
        private PluginManager _pluginManager;

        public PluginService(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        [Security(UserAccountRoles.Admin)]
        public IEnumerable<PluginDescription> GetPlugins()
        {
            return _pluginManager.GetPlugins();
        }

        [Security(UserAccountRoles.Admin)]
        public async Task UnloadPluginAsync(Guid pluginId)
        {
            await _pluginManager.UnloadPluginAsync(pluginId);
        }
        [Security(UserAccountRoles.Admin)]

        public async Task LoadPluginAsync(Guid pluginId)
        {
            _pluginManager.ActivatePlugin(pluginId);
        }
    }
}
