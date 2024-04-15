using IronRuby;
using Microsoft.Scripting.Hosting;

namespace LagDaemon.YAMUD.Automation
{
    public class IronRubyExecutor : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ScriptEngine _rubyEngine;

        public IronRubyExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _rubyEngine = Ruby.CreateEngine();
        }

        public void ExecuteScript(string script, IEnumerable<string> dependencies)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                var rubyScope = _rubyEngine.CreateScope();

                // Provide access to services via Ruby scope
                rubyScope.SetVariable("services", _serviceProvider);

                // Execute the script
                _rubyEngine.Execute(script, rubyScope);
            });
        }

        public void Dispose()
        {
            _rubyEngine.Runtime.Shutdown();
        }
    }
}

