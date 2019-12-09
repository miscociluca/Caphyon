using Caphyon.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caphyon.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        #region repositories
        IToDoRepository ToDo { get; }

        #endregion

        void BeginTransaction();

        int Commit();

        Task<int> CommitAsync();

        void Rollback();
    }
}
