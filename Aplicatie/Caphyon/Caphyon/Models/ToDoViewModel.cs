using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caphyon.Models
{
    public class ToDoViewModel
    {
        public int Id { get; set; }
        public string Descriere { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool Status { get; set; }
    }
}