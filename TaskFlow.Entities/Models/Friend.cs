﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Core.Abstract;
using TaskFlow.Entities.Base;

namespace TaskFlow.Entities.Models
{
    public class Friend:BaseEntity, IEntity
    { 
        public string? UserId { get; set; }
        public string? UserFriendId { get; set; }

        public virtual User? User { get; set; }  
        public virtual User? UserFriend { get; set; }  

    }
}
