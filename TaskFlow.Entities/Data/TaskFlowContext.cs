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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.UserFriend)
                .WithMany(u => u.FriendsOf)
                .HasForeignKey(f => f.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
      .HasOne(m => m.Sender)
      .WithMany(u => u.MessagesSender)
      .HasForeignKey(m => m.SenderId)
      .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.MessagesReceiver)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskForUser>()
       .HasOne(t => t.CreatedBy)
       .WithMany(u => u.TaskForUsers)
       .HasForeignKey(t => t.CreatedById)
       .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TaskAssigne>()
                .HasOne(ta => ta.User)
                .WithMany(u => u.TaskAssignees)
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TaskAssigne>()
                .HasOne(ta => ta.TaskForUser)
                .WithMany(t => t.TaskAssignees)
                .HasForeignKey(ta => ta.TaskForUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.User)
                .WithMany(u => u.TeamMembers)
                .HasForeignKey(tm => tm.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Project)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(tm => tm.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);


        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<TaskForUser> Tasks { get; set; }
        public virtual DbSet<Friend> Teams { get; set; }
        public virtual DbSet<TeamMember> TeamMembers { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<TaskAssigne> TaskAssignes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }


    }
}
