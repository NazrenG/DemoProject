using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class Team:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
