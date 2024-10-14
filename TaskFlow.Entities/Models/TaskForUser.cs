using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Abstract;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class TaskForUser: BaseEntity, IEntity
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime Deadline { get; set; }
        public string? Status { get; set; }

        public string? Priority { get; set; }// Urgent, Primary, Simple
        public int ProjectId { get; set; }

        public int CreatorId { get; set; }
        //

        public virtual List<User>? AssignedUsers { get; set; }
        public virtual User? User { get; set; }
        public virtual Project? Project { get; set; }
    }
}
