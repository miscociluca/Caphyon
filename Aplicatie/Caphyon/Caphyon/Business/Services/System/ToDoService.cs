using Caphyon.Business.Data.Interfaces;
using Caphyon.Business.Factories;
using Caphyon.Business.Services.System.Interfaces;
using Caphyon.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Caphyon.Business.Services.System
{
    public class ToDoService : BaseDataService<IToDo>, IToDoService
    {
        private readonly ToDoFactory _ToDoFactory;
        private readonly IToDoRepository _ToDoRepository;

        public ToDoService(DbContext context) : base(context)
        {
            _ToDoFactory = new ToDoFactory();
            _ToDoRepository = unitOfWork.ToDo;
        }

        public override IToDo Add(IToDo entity)
        {
            try
            {
                entity.CreationDate = DateTime.Now;
                entity.Status = false;
                var entityData = _ToDoFactory.CopyTo(entity);
                _ToDoRepository.AddOrUpdate(entityData);
                Commit();
                return _ToDoFactory.CopyFrom(entityData);
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return null;
        }

        public bool DeleteAll()
        {
            try
            {
                foreach (var item in _ToDoRepository.GetAll())
                {
                    _ToDoRepository.Remove(item);
                    Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return false;
        }
        public bool CompleteAll()
        {
            try
            {
                foreach (var item in _ToDoRepository.GetAll().Where(x=>x.Status==false))
                {
                     item.Status = true;
                    _ToDoRepository.AddOrUpdate(item);
                    Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return false;
        }
        public override bool Delete(int id)
        {
            try
            {
                var data = _ToDoRepository.Find(x => x.Id == id).FirstOrDefault();
                _ToDoRepository.Remove(data);
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return false;
        }
        public bool CompleteTask(int id)
        {
            try
            {
                var data = _ToDoRepository.Find(x => x.Id == id).FirstOrDefault();
                data.Status = true;
                _ToDoRepository.AddOrUpdate(data);
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return false;
        }
        public bool InCompleteTask(int id)
        {
            try
            {
                var data = _ToDoRepository.Find(x => x.Id == id).FirstOrDefault();
                data.Status = false;
                _ToDoRepository.AddOrUpdate(data);
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return false;
        }
        public override IEnumerable<IToDo> GetAll()
        {
            try
            {
                var post = _ToDoRepository.GetAll()
                   .Select(x => _ToDoFactory.CopyFrom(x))
                   .ToList();

                return post;
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return null;
        }

        public override IToDo GetById(int id)
        {
            try
            {
                return _ToDoFactory.CopyFrom(_ToDoRepository.Get(id));
            }
            catch (Exception ex)
            {
                //LogService.Log.Error(ex);
            }
            return null;
        }

        public override IEnumerable<IToDo> GetById(List<string> id)
        {
            throw new NotImplementedException();
        }

        public override IToDo Update(IToDo entity)
        {
            try
            {
                entity.CreationDate = DateTime.Now;
                var res = _ToDoFactory.CopyTo(entity);
                _ToDoRepository.AddOrUpdate(res);
                Commit();
                return entity;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}