﻿using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class User:BaseEntity
    {
        
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? AgeGroup { get; set; } //bu ne ucun lazimdir??
        public string?Phone { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

    }
}
