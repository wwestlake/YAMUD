using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebAPI.Services.Scripting
{
    public class ScriptingModuleService : IScriptingModuleService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Module> _repository;

        public ScriptingModuleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Module>();
        }

        [Security(Model.UserAccountRoles.Admin)]
        public async Task<IEnumerable<Module>> GetAll()
        {
            return await _repository.GetAsync();
        }

        [Security(Model.UserAccountRoles.Admin)]
        public async Task<Module?> Get(Guid id)
        {
            return (await _repository.GetAsync(m => m.Id == id)).FirstOrDefault();
        }

        [Security(Model.UserAccountRoles.Admin)]
        public async Task<int> Create(Module module)
        {
            _repository.Insert(module);
            return await _unitOfWork.SaveChangesAsync();
        }

        [Security(Model.UserAccountRoles.Admin)]
        public async Task Update(Module module)
        {
            _repository.Update(module);
            await _unitOfWork.SaveChangesAsync();
        }

        [Security(Model.UserAccountRoles.Admin)]
        public async Task Delete(Guid id)
        {
            var module = await Get(id);
            if (module != null)
            {
                _repository.Delete(module);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
