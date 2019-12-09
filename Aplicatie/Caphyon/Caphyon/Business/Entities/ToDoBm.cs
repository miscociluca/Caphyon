using Caphyon.Business.Data.Interfaces;
using Caphyon.Business.Entities.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caphyon.Business.Entities
{
    public class ToDoBm : IToDo
    {
        public int Id { get; set; }
        public string Descriere { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; }
    }
}