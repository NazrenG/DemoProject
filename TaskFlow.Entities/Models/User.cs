using TaskFlow.Core.Abstract;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class User:BaseEntity,IEntity
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Phone { get; set; }
        public string? Occupation { get; set; } 
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Image { get; set; }
        public string? Gender { get; set; }
        public bool? IsOnline { get; set; } 
        public DateTime? Birthday { get; set; } 
        public DateTime? LastLoginDate { get; set; }
       

        public virtual List<Project>? Projects { get; set; } 
        public virtual List<TaskForUser>? TaskForUsers { get; set; } 
        public virtual List<TeamMember>? TeamMembers { get; set; }   
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<TaskAssigne>? TaskAssignees { get; set; }

    }
}
