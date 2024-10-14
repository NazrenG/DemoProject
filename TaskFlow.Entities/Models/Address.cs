using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Abstract;

namespace TaskFlow.Entities.Models
{
    //Her User-in bir addresi ola biler ve Address-de ancaq bir usere beraber ola biler.
    //Niye var?
    public class Address:IEntity
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
