﻿using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Scripting;
using LagDaemon.YAMUD.WebAPI.Utilities;
using Microsoft.AspNetCore.Authentication;

namespace LagDaemon.YAMUD.WebAPI.Services.Scripting
{
    public class ScriptingModuleService : IScriptingModuleService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Module> _repository;
        private IUserAccountService _userAccountService;

        public ScriptingModuleService(IUnitOfWork unitOfWork, IUserAccountService userAccountService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Module>();
            _userAccountService = userAccountService;
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
            var userId = (await _userAccountService.GetCurrentUser())
                .OnSuccess(x => {
                    module.UserAccountId = x.ID;
                    _repository.Insert(module);
                }).OnFailure(x => throw new UnauthorizedAccessException());

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
