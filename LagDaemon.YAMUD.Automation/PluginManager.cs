using LagDaemon.YAMUD.Model.Utilities;
using System.Reflection;

namespace LagDaemon.YAMUD.Automation;

public class PluginManager
{
    public event PluginStoppedEventHandler PluginStopped;

    private Dictionary<Guid, IPlugin> _plugins = new Dictionary<Guid, IPlugin>();
    private IServiceProvider _serviceProvider;
    private List<PluginDescription> _pluginDescriptions = new List<PluginDescription>();

    public PluginManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<PluginDescription> GetPlugins()
    {
        return _pluginDescriptions;
    }

    public async Task UnloadPluginAsync(Guid pluginId)
    {
        var pluginToRemove = _plugins[pluginId];
        if (pluginToRemove != null)
        {
            var pd = _pluginDescriptions.FirstOrDefault(x =>
            {
                ArgumentNullException.ThrowIfNull(x);
                return x.Id == pluginId;
            });
            if (pd != null)
            {
                pd.IsActive = false;
            }
            pluginToRemove.Stopped -= PluginStoppedHandler;
            await pluginToRemove.Stop();
            _plugins.Remove(pluginToRemove.Id);
        }
    }

    private void PluginStoppedHandler(object sender, EventArgs e)
    {
        // Perform any necessary actions when a plugin stops
        // For example, you can unload the plugin manager if all plugins have stopped
        var plugin = sender as IPlugin;
        if (plugin != null)
        {
            plugin.GetDescription.IsActive = false;
            var pd = _pluginDescriptions.FirstOrDefault(x => x.Id == plugin.Id);
            if (pd != null) 
            { 
                pd.IsActive = false; 
            }
        }
        OnPluginStopped();
    }

    protected virtual void OnPluginStopped()
    {
        PluginStopped?.Invoke(this, EventArgs.Empty);
    }

    public void LoadPlugins(string directory)
    {
        if (! Directory.Exists(directory))
        {
            return; // there are no plugins
        }
        foreach (var file in Directory.GetFiles(directory, "*.dll"))
        {
            var assembly = Assembly.LoadFrom(file);
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    var constructors = type.GetConstructors();
                    var parameters = constructors[0].GetParameters(); // Assume only one constructor for simplicity

                    // Resolve dependencies dynamically
                    var dependencies = parameters.Select(param => _serviceProvider.GetService(param.ParameterType)).ToArray();

                    // Instantiate plugin with resolved dependencies
                    var plugin = Activator.CreateInstance(type, dependencies) as IPlugin;
                    if (plugin == null)
                    {
                        throw new Exception($"Failed to create instance of plugin {type.FullName}");
                    }
                    _plugins.Add(plugin.Id,  plugin);
                    plugin.Stopped += PluginStoppedHandler;
                    plugin.Initialize();
                    plugin.GetDescription.IsActive = true;
                    plugin.GetDescription.AssemblyPath = file;
                    _pluginDescriptions.Add(plugin.GetDescription);
                }
            }
        }
    }
}
