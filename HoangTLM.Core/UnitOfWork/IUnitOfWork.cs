using System;
using System.Threading.Tasks;

namespace HoangTLM.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
} 