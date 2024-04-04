using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface IScriptingModuleService
    {
        Task CreateNewModule(Module module);
        Task DeleteModule(Guid id);
        Task<IEnumerable<Module>> GetAllModules();
        Task<Module> GetModuleById(Guid id);
        Task UpdateModule(Module module);
    }
}
