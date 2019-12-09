using Caphyon.Infrastructure.Repositories;
using Caphyon.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Caphyon.Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        #region private properties
        private readonly DbContext _context;
        #endregion

        #region members
        private IToDoRepository _posts;
        public IToDoRepository ToDo
        {
            get
            {
                if (_posts == null)
                {
                    _posts = new ToDoRepository(_context);
                }
                return _posts;
            }
        }

        #endregion

        #region Ctor's
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        #endregion

        #region public methods
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var msg = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //LogService.Log.Error(msg);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var prop = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        //LogService.Log.Error(prop);
                    }
                }
                throw new Exception(e.GetBaseException().Message);
            }
            catch (DbUpdateException e)
            {
                //LogService.Log.Error(e.Message);
                Rollback();

                throw new Exception(e.GetBaseException().Message);
            }
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}