using Microsoft.EntityFrameworkCore;
using TaskFlow.Entities.Models;
using TaskForUser = TaskFlow.Entities.Models.TaskForUser;

namespace TaskFlow.Entities.Data
{
    public class TaskFlowContext : DbContext
    {
        public TaskFlowContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TaskForUser> Tasks { get; set; }
        public virtual DbSet<Friend> Teams { get; set; }  
        public virtual DbSet<TeamMember> TeamMembers { get; set; }

    }
}
