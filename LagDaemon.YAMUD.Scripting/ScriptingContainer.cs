using IronPython.Hosting;
using IronRuby;
using Microsoft.Scripting.Hosting;

namespace LagDaemon.YAMUD.Scripting
{
    public class ScriptingContainer
    {
        private ScriptEngine _engine;
        private ScriptScope _scope;

        private ScriptingContainer(ScriptEngine engine, ScriptScope scope)
        {
            _engine = engine;
            _scope = scope;
        }

        public static ScriptingContainer CreatePythonContainer()
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            return new ScriptingContainer(engine, scope);
        }

        public static ScriptingContainer CreateRubyContainer()
        {
            var engine = Ruby.CreateEngine();
            var scope = engine.CreateScope();
            return new ScriptingContainer(engine, scope);
        }

        public void Execute(string code)
        {
            _engine.Execute(code, _scope);
        }

        public dynamic GetVariable(string variableName)
        {
            return _scope.GetVariable(variableName);
        }

        public void SetVariable(string variableName, dynamic value)
        {
            _scope.SetVariable(variableName, value);
        }

    }
}
