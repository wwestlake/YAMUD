using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Automation;

public class PluginManager
{
    private List<IPlugin> _plugins = new List<IPlugin>();
    private IServiceProvider _serviceProvider;

    public PluginManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
                    _plugins.Add(plugin);
                    plugin.Initialize();
                }
            }
        }
    }
}
