using Caphyon.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Caphyon.Infrastructure.Repositories
{
    public class ToDoRepository : BaseRepository<ToDoList>, IToDoRepository
    {
        public ToDoRepository(DbContext context) : base(context)
        {
        }
    }
}