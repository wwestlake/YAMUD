using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebAPI.Services.Scripting
{
    public interface IScriptingModuleService
    {
        Task<int> Create(Module module);
        Task Delete(Guid id);
        Task<Module?> Get(Guid id);
        Task<IEnumerable<Module>> GetAll();
        Task Update(Module module);
    }
}
