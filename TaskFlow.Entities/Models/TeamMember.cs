using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    // Team ve User class-inin coxun coxa relation-undan emele gelen 3-cu class-dir.

    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }

        public int UserId { get; set; }
        public string? TeamRole { get; set; }

        ///
        public virtual User? User { get; set; }
        public virtual Team? Team { get; set; }
    }
}
