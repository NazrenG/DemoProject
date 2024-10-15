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
            // Kullanıcının arkadaş eklediği kişiler (UserId ile ilişkilendirilir)
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)  // Kullanıcının arkadaş eklediği kişiler
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Kullanıcının arkadaş olarak eklendiği kişiler (UserFriendId ile ilişkilendirilir)
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.UserFriend)
                .WithMany(u => u.FriendsOf)  // Kullanıcının arkadaş olarak eklendiği kişiler
                .HasForeignKey(f => f.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
      .HasOne(m => m.Sender)
      .WithMany(u => u.MessagesSender)  // Gönderenin mesajları
      .HasForeignKey(m => m.SenderId)
      .OnDelete(DeleteBehavior.Restrict);

            // Receiver ile ilişkiyi yapılandırma
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.MessagesReceiver)  // Alıcının mesajları
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Comment>Comments { get; set; }
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
