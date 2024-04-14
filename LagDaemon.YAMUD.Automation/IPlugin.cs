using LagDaemon.YAMUD.Model.Utilities;

namespace LagDaemon.YAMUD.Automation
{
    public delegate void PluginStoppedEventHandler(object sender, EventArgs e);

    public interface IPlugin
    {
        Guid Id { get; }
        event PluginStoppedEventHandler Stopped;
        void Initialize();
        PluginDescription GetDescription { get; }
        Task Stop();
    }
}
