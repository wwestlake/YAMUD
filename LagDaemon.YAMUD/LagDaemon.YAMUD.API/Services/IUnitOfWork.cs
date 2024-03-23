﻿namespace LagDaemon.YAMUD.API.Services
{

    namespace LagDaemon.YAMUD.API
    {
        public interface IUnitOfWork : IDisposable
        {
            IRepository<T> GetRepository<T>() where T : class;
            int SaveChanges();
            Task<int> SaveChangesAsync();
        }
    }


}
