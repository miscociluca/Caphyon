using AutoMapper;
using Caphyon.Business.Data.Interfaces;
using Caphyon.Business.Entities;
using Caphyon.Business.Factories.Foundation;
using Caphyon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caphyon.Business.Factories
{
    public class ToDoFactory : IDataFactory<ToDoList, IToDo>
    {
        public ToDoFactory()
        {

        }

        public IToDo CopyFrom(ToDoList daObject)
        {
            return daObject != null
                ? new ToDoBm
                {
                    Id = daObject.Id,
                    CreationDate = daObject.CreationDate,
                    Descriere = daObject.Descriere,
                    Status = daObject.Status
                } : null;
        }

        public ToDoList CopyTo(IToDo entity)
        {
            return new ToDoList
            {
                Id = entity.Id,
                CreationDate = entity.CreationDate,
                Descriere = entity.Descriere,
                Status = entity.Status
            };
        }
    }
}