using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebAPI.Services.ScriptingServices;

public interface IScriptingModuleService
{
    Task<int> Create(CodeModule module);
    Task Delete(Guid id);
    Task<CodeModule?> Get(Guid id);
    Task<IEnumerable<CodeModule>> GetAll();
    Task Update(CodeModule module);
}
