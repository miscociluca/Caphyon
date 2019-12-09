using Caphyon.Business.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caphyon.Business.Services.System.Interfaces
{
    public interface IToDoService : IService<IToDo>
    {
        bool DeleteAll();
        bool CompleteAll();
        bool CompleteTask(int id);
        bool InCompleteTask(int id);
    }
}
