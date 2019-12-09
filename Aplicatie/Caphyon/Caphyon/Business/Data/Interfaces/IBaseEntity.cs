using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caphyon.Business.Data.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        //void CopyFrom(IBaseEntity entity);
    }
}
