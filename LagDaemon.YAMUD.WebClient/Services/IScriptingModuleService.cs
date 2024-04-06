using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebClient.Services
{
    public interface IScriptingModuleService
    {
        Task CreateNewModule(CodeModule module);
        Task DeleteModule(Guid id);
        Task<IEnumerable<CodeModule>> GetAllModules();
        Task<CodeModule> GetModuleById(Guid id);
        Task UpdateModule(CodeModule module);
    }
}
