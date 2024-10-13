using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Entities.Models
{
    //Her User-in bir addresi ola biler ve Address-de ancaq bir usere beraber ola biler.

    public class Address
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
