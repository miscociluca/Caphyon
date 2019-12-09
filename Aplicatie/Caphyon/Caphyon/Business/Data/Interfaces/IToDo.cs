using Caphyon.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caphyon.Business.Data.Interfaces
{
    public interface IToDo
    {
        int Id { get; set; }
        string Descriere { get; set; }
        DateTime? CreationDate { get; set; }
        bool Status { get; set; }
    }
}
